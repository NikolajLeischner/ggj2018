using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnergyReceiver : MonoBehaviour {

	public abstract void AddEnergy (float addedEnergy, EnergyType energyType);
}
