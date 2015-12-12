using System;
using UnityEngine;

public class NormalDistribution : MonoBehaviour {
	private static readonly System.Random seedGenerator = new System.Random();
	public double mean;
	public double dev;

	private System.Random random = new System.Random(seedGenerator.Next());

	public double NextNormal() {
		return mean + dev * Math.Sqrt(-2.0 * Math.Log(random.NextDouble())) *
			Math.Sin(2.0 * Math.PI * random.NextDouble());
	}
}
