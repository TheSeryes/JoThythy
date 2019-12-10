using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowUIDisplay : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI m_Text;

    private void Update()
    {
        m_Text.text = GameManager.Instance.Player.ArrowAmount.ToString();
    }
}
