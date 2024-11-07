using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public AudioSource Deathsound;
    public AudioSource Stage1Bgm;
    public GameObject retryScreen;  // Retry 화면
    public Text deathCountText;  // Death Count를 표시할 UI 텍스트
    public int deathCount = 0;  // Death Count 변수
    bool isDie;
    private static GameManager instance;  // 싱글톤 패턴을 위한 변수

    // 게임을 처음 시작할 때 또는 씬 전환 시 호출
    private void Awake()
    {
        Deathsound = GetComponent<AudioSource>();
        Stage1Bgm = GetComponent<AudioSource>();
        isDie = false;
        Stage1Bgm.Play();
        Stage1Bgm.loop = true;
        

        // 게임 시작 시 PlayerPrefs에서 저장된 deathCount 불러오기
        deathCount = PlayerPrefs.GetInt("Death", 0);
        UpdateDeathCountUI();  // UI 업데이트
    }

    private void Start()
    {
        // 게임 시작 시 retryScreen 비활성화 (게임 시작 화면에서는 retryScreen이 안 보이게)
        if(retryScreen != null)
        {
            retryScreen.SetActive(false);
        }
    }

    // 플레이어가 죽었을 때 호출되는 메서드
    public void Die()
    {
        if(deathCountText != null)
        {
            if (!isDie)
            {
                isDie = true;
                Stage1Bgm.Stop();
                Deathsound.Play();
                Time.timeScale = 0;  // 게임 일시 정지
                retryScreen.SetActive(true);  // Retry 화면 표시
                deathCount++;  // 죽을 때마다 deathCount 증가
                PlayerPrefs.SetInt("Death", deathCount);  // Death Count 저장
                UpdateDeathCountUI();  // Death Count UI 업데이트
            }
        }
    }

    // 게임을 재시작하는 메서드
    public void Retry()
    {
        Time.timeScale = 1;  // 게임 시간 다시 정상화
        SceneManager.LoadScene("Stage1");  // Stage1 씬 로드
    }

    // 충돌 시 처리하는 메서드
    

    // Death Count UI 업데이트
    private void UpdateDeathCountUI()
    {
        if (deathCountText != null)
        {
            deathCountText.text = "Death Count: " + deathCount.ToString();  // deathCount UI에 표시
        }
    }
}
