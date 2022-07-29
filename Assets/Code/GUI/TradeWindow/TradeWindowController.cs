using TMPro;
using UnityEngine;

public class TradeWindowController : GUIWindow
{
    public class TradeWindowContext : WindowContext
    {
        public string ItemName;
        public string ItemPrice;
    }

    [Header("Window parts")]
    [SerializeField] 
    private Animator m_animator;

    [SerializeField] 
    private TextMeshProUGUI m_windowText;

    [Header("Params")] 
    [SerializeField] 
    private string m_itemNameParam;

    [SerializeField] 
    private string m_itemPriceParam;

    private string m_originalText;

    protected override void AwakeInternal()
    {
        m_originalText = m_windowText.text;
    }

    protected override void OpenInternal(WindowContext context)
    {
        var concreteContext = context as TradeWindowContext;

        var newText = m_originalText;
        newText = newText.Replace(m_itemNameParam, concreteContext.ItemName);
        newText = newText.Replace(m_itemPriceParam, concreteContext.ItemPrice);

        m_windowText.text = newText;
        m_animator.Play("Open");
    }

    protected override void CloseInternal()
    {
        m_animator.Play("Close");
    }
}
