using UnityEngine;
using System.IO; // 파일 저장/불러오기
using System;

[System.Serializable]
public class UserData
{
    public string userName;
    public int cash_money; // 현금
    public int money;      // 통장 잔액

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
            Debug.Log($"{amount}원을 입금했습니다.");
        }
        else
        {
            Debug.Log("현금이 부족합니다.");
        }
    }

    public void Withdraw(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            cash_money += amount;
            Debug.Log($"{amount}원을 출금했습니다.");
        }
        else
        {
            Debug.Log("잔액이 부족합니다.");
        }
    }

    // 저장 함수
    public void Save()
    {
        string json = JsonUtility.ToJson(this, true);
        File.WriteAllText(saveFile, json);
        Debug.Log("사용자 데이터 저장 완료");
    }

    // 불러오기 함수
    public static UserData Load()
    {
        if (File.Exists(saveFile))
        {
            string json = File.ReadAllText(saveFile);
            UserData loadedData = JsonUtility.FromJson<UserData>(json);
            Debug.Log("사용자 데이터 불러오기 완료");
            return loadedData;
        }
        else
        {
            Debug.LogWarning("저장된 데이터가 없습니다. 새로 생성합니다.");
            return new UserData("새 유저", 50000, 10000);
        }
    }
}
