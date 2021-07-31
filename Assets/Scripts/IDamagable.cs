using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeCount(int count);
    void Die(bool effect);

} 
