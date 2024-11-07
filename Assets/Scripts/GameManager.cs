using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public AudioSource Deathsound;
    public AudioSource Stage1Bgm;
    public GameObject retryScreen;  // Retry ȭ��
    public Text deathCountText;  // Death Count�� ǥ���� UI �ؽ�Ʈ
    public int deathCount = 0;  // Death Count ����
    bool isDie;
    private static GameManager instance;  // �̱��� ������ ���� ����

    // ������ ó�� ������ �� �Ǵ� �� ��ȯ �� ȣ��
    private void Awake()
    {
        Deathsound = GetComponent<AudioSource>();
        Stage1Bgm = GetComponent<AudioSource>();
        isDie = false;
        Stage1Bgm.Play();
        Stage1Bgm.loop = true;
        

        // ���� ���� �� PlayerPrefs���� ����� deathCount �ҷ�����
        deathCount = PlayerPrefs.GetInt("Death", 0);
        UpdateDeathCountUI();  // UI ������Ʈ
    }

    private void Start()
    {
        // ���� ���� �� retryScreen ��Ȱ��ȭ (���� ���� ȭ�鿡���� retryScreen�� �� ���̰�)
        if(retryScreen != null)
        {
            retryScreen.SetActive(false);
        }
    }

    // �÷��̾ �׾��� �� ȣ��Ǵ� �޼���
    public void Die()
    {
        if(deathCountText != null)
        {
            if (!isDie)
            {
                isDie = true;
                Stage1Bgm.Stop();
                Deathsound.Play();
                Time.timeScale = 0;  // ���� �Ͻ� ����
                retryScreen.SetActive(true);  // Retry ȭ�� ǥ��
                deathCount++;  // ���� ������ deathCount ����
                PlayerPrefs.SetInt("Death", deathCount);  // Death Count ����
                UpdateDeathCountUI();  // Death Count UI ������Ʈ
            }
        }
    }

    // ������ ������ϴ� �޼���
    public void Retry()
    {
        Time.timeScale = 1;  // ���� �ð� �ٽ� ����ȭ
        SceneManager.LoadScene("Stage1");  // Stage1 �� �ε�
    }

    // �浹 �� ó���ϴ� �޼���
    

    // Death Count UI ������Ʈ
    private void UpdateDeathCountUI()
    {
        if (deathCountText != null)
        {
            deathCountText.text = "Death Count: " + deathCount.ToString();  // deathCount UI�� ǥ��
        }
    }
}
