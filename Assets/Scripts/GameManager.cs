using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject retryScreen;  // Retry ȭ��
    public Text deathCountText;  // Death Count�� ǥ���� UI �ؽ�Ʈ
    public int deathCount = 0;  // Death Count ����

    private static GameManager instance;  // �̱��� ������ ���� ����

    // ������ ó�� ������ �� �Ǵ� �� ��ȯ �� ȣ��
    private void Awake()
    {
  
        
        

        // ���� ���� �� PlayerPrefs���� ����� deathCount �ҷ�����
        deathCount = PlayerPrefs.GetInt("Death", 0);
        UpdateDeathCountUI();  // UI ������Ʈ
    }

    private void Start()
    {
        // ���� ���� �� retryScreen ��Ȱ��ȭ (���� ���� ȭ�鿡���� retryScreen�� �� ���̰�)
        retryScreen.SetActive(false);
    }

    // �÷��̾ �׾��� �� ȣ��Ǵ� �޼���
    public void Die()
    {
        Time.timeScale = 0;  // ���� �Ͻ� ����
        retryScreen.SetActive(true);  // Retry ȭ�� ǥ��
        deathCount++;  // ���� ������ deathCount ����
        PlayerPrefs.SetInt("Death", deathCount);  // Death Count ����
        UpdateDeathCountUI();  // Death Count UI ������Ʈ
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
