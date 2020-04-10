using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI potionAmount;
    [SerializeField] private TextMeshProUGUI kunaiAmount;
    [SerializeField] private TextMeshProUGUI shurikenAmount;
    
    private Inventory inventory;
    

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
    void UpdateUI()
    {
        potionAmount.text = inventory.GetPotionCount().ToString();
        kunaiAmount.text = inventory.GetKunaiCount().ToString();
        shurikenAmount.text = inventory.GetShurikenCount().ToString();
    }
}
