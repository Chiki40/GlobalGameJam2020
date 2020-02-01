using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestClickableComponent : ClickableComponent
{
    private bool _alreadyLaunched = false;
    public void lolazo()
    {
        if (!_alreadyLaunched)
        {
            _alreadyLaunched = true;
            FindObjectOfType<ConversationManager>().SetConversation(new List<string>(){"loooool", "laaaaaal", "leeeeeeeeel", "luuuuuul", "liiiil"});
        }
        else
        {
            FindObjectOfType<ConversationManager>().NextMessage();
        }
    }
}
