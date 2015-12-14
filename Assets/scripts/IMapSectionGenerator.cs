using UnityEngine;
using System.Collections;

public interface IMapSectionGenerator {
	float GenerateSection(float difficulty, float lastX);
}
