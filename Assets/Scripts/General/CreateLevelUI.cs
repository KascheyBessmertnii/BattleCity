using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateLevelUI : MonoBehaviour
{
    [Header("Main map settings")]
    [SerializeField] protected Vector2Int mapSize = new Vector2Int(26, 26);
    [SerializeField] protected int squareSize = 1;
    [SerializeField, Range(-10f, 10f)] protected float offsetX = 0;
    [SerializeField, Range(-10f, 10f)] protected float offsetZ = 0;
    [SerializeField] protected LayerMask buildLayer;

    [SerializeField, Header("Parrent for walls")]
    protected Transform wallParent;

    [Header("UI create buttons")]
    [SerializeField] protected Button btnCreateWall;
    [SerializeField] protected Button btnCreateBrone;
    [Space(10)]
    [SerializeField] protected Button btnSave;
    [SerializeField] protected Button btnClose;
    [Space(10)]
    [SerializeField] protected TMP_InputField iLevelNum;

    [Header("Warning panel")]
    [SerializeField] protected GameObject warningPanel;
}
