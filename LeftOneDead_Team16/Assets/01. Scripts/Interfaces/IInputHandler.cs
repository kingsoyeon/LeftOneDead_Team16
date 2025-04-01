using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    void OnEscPressed(); // ESC 눌렀을 때
    void OnScroll(); // 마우스의 스크롤 내릴 때
}
