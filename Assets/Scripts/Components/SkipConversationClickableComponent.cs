using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipConversationClickableComponent : MonoBehaviour
{
    [SerializeField]
    private ConversationManager _conversationManager = null;

    private void Awake()
    {
        if (_conversationManager == null)
        {
             Debug.LogError("[SkipConversation.Awake] ERROR: Serializable _conversationManager not set");
            return;
        }
    }
    public void SkipConversation()
    {
        _conversationManager.NextMessage();
    }
}
