using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UserData userData;

    public Text cashText;
    public Text balanceText;
    public Text nameText;

    // 입출금 UI 패널
    public GameObject depositPanel;
    public GameObject withdrawPanel;
    public GameObject mainPanel;

    public TMP_InputField depositInput;
    public TMP_InputField withdrawInput;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        userData = UserData.Load();
        UpdateUI();
        ShowMainPanel();

    }

    public void UpdateUI()
    {
        cashText.text = userData.cash_money.ToString("N0");
        balanceText.text = userData.money.ToString("N0");
        nameText.text = userData.userName;
    }

    // 입금 관련
    public void OnDepositAmount(int amount)
    {
        userData.Deposit(amount);
        UpdateUI();
    }

    public void OnDepositCustom()
    {
        if (int.TryParse(depositInput.text, out int amount))
        {
            userData.Deposit(amount);
            depositInput.text = "";
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("잘못된 입금 금액 입력!");
        }
    }

    // 출금 관련
    public void OnWithdrawAmount(int amount)
    {
        userData.Withdraw(amount);
        UpdateUI();
    }

    public void OnWithdrawCustom()
    {
        if (int.TryParse(withdrawInput.text, out int amount))
        {
            userData.Withdraw(amount);
            withdrawInput.text = "";
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("잘못된 출금 금액 입력!");
        }
    }

    // 패널 전환
    public void ShowDepositPanel()
    {
        depositPanel.SetActive(true);
        withdrawPanel.SetActive(false);
        mainPanel.SetActive(false);
    }

    public void ShowWithdrawPanel()
    {
        depositPanel.SetActive(false);
        withdrawPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void ShowMainPanel()
    {
        depositPanel.SetActive(false);
        withdrawPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void SaveUserData()
    {
        userData.Save();
    }

    //public void LodeUserData()
    //{
    //    UserData.Load();
    //}
}
