using CharmPills.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CharmPills.Tests;

[TestClass]
public class DamageReductionTests
{
	[DataTestMethod]
	[DataRow(10, 0.5f, 5)]
	[DataRow(20, 0.5f, 10)]
	[DataRow(5, 0.5f, 3)]
	[DataRow(3, 0.5f, 2)]
	[DataRow(100, 0.1f, 10)]
	[DataRow(7, 1f, 7)]
	public void Reduce_RoundsToNearestWholePoint(int damage, float multiplier, int expected)
	{
		Assert.AreEqual(expected, DamageReduction.Reduce(damage, multiplier));
	}

	[DataTestMethod]
	[DataRow(1, 0.5f)]
	[DataRow(1, 0.1f)]
	[DataRow(2, 0.1f)]
	[DataRow(4, 0.1f)]
	public void Reduce_NeverNullifiesAHitOfAtLeastOne(int damage, float multiplier)
	{
		Assert.IsTrue(DamageReduction.Reduce(damage, multiplier) >= 1);
	}

	[TestMethod]
	public void Reduce_NeverRaisesDamage()
	{
		Assert.AreEqual(5, DamageReduction.Reduce(5, 3f));
		Assert.AreEqual(1, DamageReduction.Reduce(1, 1f));
	}

	[TestMethod]
	public void Reduce_ClampsNegativeMultiplier()
	{
		Assert.AreEqual(1, DamageReduction.Reduce(9, -1f));
	}

	[TestMethod]
	public void Reduce_LeavesNonPositiveDamageAlone()
	{
		Assert.AreEqual(0, DamageReduction.Reduce(0, 0.5f));
		Assert.AreEqual(-3, DamageReduction.Reduce(-3, 0.5f));
	}

	[TestMethod]
	public void IsWorthReducing_HonoursMinimumHit()
	{
		Assert.IsFalse(DamageReduction.IsWorthReducing(0, 1f));
		Assert.IsTrue(DamageReduction.IsWorthReducing(1, 1f));
		Assert.IsFalse(DamageReduction.IsWorthReducing(4, 5f));
		Assert.IsTrue(DamageReduction.IsWorthReducing(5, 5f));
	}
}
