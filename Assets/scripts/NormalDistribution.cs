using System;
using UnityEngine;

public class NormalDistribution {
	private static readonly System.Random seedGenerator = new System.Random();

	public static readonly NormalDistribution STD = new NormalDistribution(0,1);

	public double mean;
	public double dev;

	public NormalDistribution() {}

	public NormalDistribution(double m, double d) {
		mean = m;
		dev = d;
	}

	private System.Random random = new System.Random(seedGenerator.Next());

	public double NextNormal() {
		return mean + dev * Math.Sqrt(-2.0 * Math.Log(random.NextDouble())) *
			Math.Sin(2.0 * Math.PI * random.NextDouble());
	}
}
