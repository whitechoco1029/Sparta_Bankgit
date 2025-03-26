using UnityEngine;
using System.IO; // ���� ����/�ҷ�����
using System;

[System.Serializable]
public class UserData
{
    public string userName;
    public int cash_money; // ����
    public int money;      // ���� �ܾ�

    private static string saveFile => Path.Combine(Application.persistentDataPath, "userdata.json");

    public UserData(string name, int cash_money, int money)
    {
        this.userName = name;
        this.cash_money = cash_money;
        this.money = money;
    }

    public void Deposit(int amount)
    {
        if (cash_money >= amount)
        {
            cash_money -= amount;
            money += amount;
            Debug.Log($"{amount}���� �Ա��߽��ϴ�.");
        }
        else
        {
            Debug.Log("������ �����մϴ�.");
        }
    }

    public void Withdraw(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            cash_money += amount;
            Debug.Log($"{amount}���� ����߽��ϴ�.");
        }
        else
        {
            Debug.Log("�ܾ��� �����մϴ�.");
        }
    }

    // ���� �Լ�
    public void Save()
    {
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(saveFile, json);
        Debug.Log("����� ������ ���� �Ϸ�");
    }

    // �ҷ����� �Լ�
    public static UserData Load()
    {
        if (File.Exists(saveFile))
        {
            string json = File.ReadAllText(saveFile);
            UserData loadedData = JsonUtility.FromJson<UserData>(json);
            Debug.Log("����� ������ �ҷ����� �Ϸ�");
            return loadedData;
        }
        else
        {
            Debug.LogWarning("����� �����Ͱ� �����ϴ�. ���� �����մϴ�.");
            return new UserData("�� ����", 50000, 10000);
        }
    }
}
