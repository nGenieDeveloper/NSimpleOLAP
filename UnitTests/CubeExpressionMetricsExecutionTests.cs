using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSimpleOLAP;
using NSimpleOLAP.Query;
using NSimpleOLAP.Query.Builder;
using NUnit.Framework;
using NSimpleOLAP.Common.Utils;
using NSimpleOLAP.CubeExpressions;

namespace UnitTests
{
  [TestFixture]
  public class CubeExpressionMetricsExecutionTests
  {
    [Test]
    public void Basic_Metric_Expression_Execution_Test()
    {
      using (var cube = CubeSourcesFixture.GetBasicCubeThreeDimensionsTwoMeasures2())
      {
        cube.Initialize();

        cube.BuildMetrics()
        .Add("teste1", exb => exb.Expression(e => e.Set("quantity").Sum(10)))
        .Create();

        Assert.IsNotNull(cube.Schema.Metrics["teste1"]);

        cube.Process();

        var cell = cube.Cells.Take(1).FirstOrDefault();


        var valueMeasure = (int)cell.Values[cube.Schema.Measures["quantity"].ID];
        var value = (int) cell.Values[cube.Schema.Metrics["teste1"].ID];

        Assert.AreEqual(valueMeasure + 10, value);
      }
    }

    [Test]
    public void Basic_Composite_Metric_Expression_Execution_Test()
    {
      using (var cube = CubeSourcesFixture.GetBasicCubeThreeDimensionsTwoMeasures2())
      {
        cube.Initialize();

        cube.BuildMetrics()
        .Add("teste2DoubleSum",
          exb => exb.Expression(e => e.Set("quantity").Sum(e2 => e2.Set("quantity").Value())))
        .Create();

        Assert.IsNotNull(cube.Schema.Metrics["teste2DoubleSum"]);

        cube.Process();

        var cell = cube.Cells.Take(1).FirstOrDefault();


        var valueMeasure = (int)cell.Values[cube.Schema.Measures["quantity"].ID];
        var value = (int)cell.Values[cube.Schema.Metrics["teste2DoubleSum"].ID];

        Assert.AreEqual(valueMeasure*2, value);
      }
    }

    [Test]
    public void Basic_Multiple_Metric_Expressions_Execution_Test()
    {
      using (var cube = CubeSourcesFixture.GetBasicCubeThreeDimensionsTwoMeasures2())
      {
        cube.Initialize();

        cube.BuildMetrics()
        .Add("testeAverage",
          exb => exb.Expression(e => e.Set("quantity").Average()))
        .Add("testeMax",
          exb => exb.Expression(e => e.Set("quantity").Max()))
        .Add("testeMin",
          exb => exb.Expression(e => e.Set("quantity").Min()))
        .Create();

        Assert.IsNotNull(cube.Schema.Metrics["testeAverage"]);
        Assert.IsNotNull(cube.Schema.Metrics["testeMax"]);
        Assert.IsNotNull(cube.Schema.Metrics["testeMin"]);

        cube.Process();

        var cell = cube.Cells.Take(1).FirstOrDefault();


        var valueMeasure = (int)cell.Values[cube.Schema.Measures["quantity"].ID];
        var valueAverage = cell.Values[cube.Schema.Metrics["testeAverage"].ID];
        var valueMax = cell.Values[cube.Schema.Metrics["testeMax"].ID];
        var valueMin = cell.Values[cube.Schema.Metrics["testeMin"].ID];

        Assert.AreEqual(valueMeasure / 24, valueAverage);
        Assert.AreEqual(101, valueMax);
        Assert.AreEqual(1, valueMin);
      }
    }
  }
}
