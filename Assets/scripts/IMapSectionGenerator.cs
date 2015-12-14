using UnityEngine;
using System.Collections;

public interface IMapSectionGenerator {
	GameObject GenerateSection(float difficulty, float xNextGenerate, GameObject lastElement);
}
