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

namespace UnitTests
{
  [TestFixture]
  public class CubeExpressionMetricsTests
  {
    private Cube<int> cube;

    public CubeExpressionMetricsTests()
    {
      Init();
    }

    public void Init()
    {
      cube = CubeSourcesFixture.GetBasicCubeThreeDimensionsTwoMeasures2();
      cube.Initialize();
    }

    [OneTimeTearDown]
    public void Dispose()
    {
      cube.Dispose();
    }

    [Test]
    public void Basic_Metric_Expression_Setup_Test()
    {
      
    }
  }
}
