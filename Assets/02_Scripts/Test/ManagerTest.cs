using System;
using Unity.VisualScripting;
using UnityEngine;

public class ManagerTest : MonoBehaviour
{
    // [ 아웃 게임 (레이어드 아키텍처) ]
    // 
    // 1. Upgrade 클래스
    // - (배치 안함)
    // - ([System.Serializable] : 클래스를 Unity가 직렬화할 수 있게 만드는 속성)
    // - monobehaviour를 상속받지 않음
    // - 업데이트 시 필요한 변수 및 함수 선언
    // - 생성자 함수에서 변수 값 변경
    // 
    // 2. UpgradeManager 클래스
    // - (씬에 배치)
    // - 관리자용 instance는 static으로 선언 (Awake()에서 instance 저장)
    // - 다양한 upgrades & 업그레이드 버튼들 배열로 받아오기
    // - 다른 매니저와의 관계가 있는 함수 선언
    // - 업그레이드 버튼들 갱신 함수 선언
    // 
    // 3. UI_UpgradeButton 클래스
    // - (UI에 배치)
    // - 버튼, 텍스트 등 UI에서 갱신해야될 것들 변수로 선언
    // - 해당 버튼의 인덱스 외부 지정용 변수 선언
    // - 버튼 클릭 시 발동되는 함수 선언
    // - 버튼 내부 UI 갱신 함수 선언

    private static ManagerTest _instance = null;
    public static ManagerTest Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }
}
