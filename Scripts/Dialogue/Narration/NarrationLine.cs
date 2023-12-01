using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Narration/Line")]
public class NarrationLine : ScriptableObject
{
    [SerializeField]
    private NarrationCharacter m_Speaker;
    [SerializeField]
    [TextArea(1, 3)]
    private string m_Text;

    public NarrationCharacter Speaker => m_Speaker;
    public string Text => m_Text;
}