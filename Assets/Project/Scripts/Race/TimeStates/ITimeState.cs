using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeState
{
    public void Open();
    public void Update();
    public void Exit();
}
