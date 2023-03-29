using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class LoginPanelView : MonoBehaviour
{
    // Debug Flag to simulate a reset
    public bool ClearPlayerPrefs;

    public Button PlayAsGuestButton;

    public GameObject LoginPanel;

    public TextMeshProUGUI StatusText;

    private CanvasGroup canvasGroup;

    // Settings for what data to get from playfab on login.
    public GetPlayerCombinedInfoRequestParams InfoRequestParams;

    // Reference to our Authentication service
    private PlayFabAuthService _AuthService = PlayFabAuthService.Instance;

    public void Awake()
    {
        if (ClearPlayerPrefs)
        {
            //_AuthService.UnlinkSilentAuth();
            _AuthService.ClearRememberMe();
            _AuthService.AuthType = Authtypes.None;
        }

        if (this.TryGetComponent(out CanvasGroup _canvasGroup))
        {
            canvasGroup = _canvasGroup;
        }
    }

    public void Start()
    {
        // Hide all our panels until we know what UI to display
        Utility.EnablePanel(canvasGroup, false);

        // Subscribe to events that happen after we authenticate
        PlayFabAuthService.OnDisplayAuthentication += OnDisplayAuthentication;
        PlayFabAuthService.OnLoginSuccess += OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError += OnPlayFaberror;

        // Bind to UI buttons to perform actions when user interacts with the UI.
        PlayAsGuestButton.onClick.AddListener(OnPlayAsGuestClicked);

        // Set the data we want at login from what we chose in our meta data.
        _AuthService.InfoRequestParams = InfoRequestParams;

        // Start the authentication process.
        _AuthService.Authenticate();
    }


    /// <summary>
    /// Login Successfully - Goes to next screen.
    /// </summary>
    /// <param name="result"></param>
    private void OnLoginSuccess(PlayFab.ClientModels.LoginResult result)
    {
        Debug.LogFormat("Logged In as: {0}", result.PlayFabId);
        StatusText.text = "";

        EventSystem.CallSuccesfullLogin();

        Utility.EnablePanel(canvasGroup, false);
    }

    /// <summary>
    /// Error handling for when Login returns errors.
    /// </summary>
    /// <param name="error"></param>
    private void OnPlayFaberror(PlayFabError error)
    {
        //There are more cases which can be caught, below are some
        //of the basic ones.
        switch (error.Error)
        {
            case PlayFabErrorCode.InvalidEmailAddress:
            case PlayFabErrorCode.InvalidPassword:
            case PlayFabErrorCode.InvalidEmailOrPassword:
                StatusText.text = "Invalid Email or Password";
                break;

            case PlayFabErrorCode.AccountNotFound:
            default:
                StatusText.text = error.GenerateErrorReport();
                break;
        }

        //Also report to debug console, this is optional.
        Debug.Log(error.Error);
        Debug.LogError(error.GenerateErrorReport());
    }

    /// <summary>
    /// Choose to display the Auth UI or any other action.
    /// </summary>
    private void OnDisplayAuthentication()
    {
        //Here we have choses what to do when AuthType is None.
        Utility.EnablePanel(canvasGroup, true);

        StatusText.text = "";

        /*
         * Optionally we could Not do the above and force login silently
         * 
         * _AuthService.Authenticate(Authtypes.Silent);
         * 
         * This example, would auto log them in by device ID and they would
         * never see any UI for Authentication.
         * 
         */
    }

    /// <summary>
    /// Play As a guest, which means they are going to silently authenticate
    /// by device ID or Custom ID
    /// </summary>
    private void OnPlayAsGuestClicked()
    {

        StatusText.text = "Logging In As Guest ...";

        _AuthService.Authenticate(Authtypes.Silent);
    }
}
