using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DungeonArchitect.SpatialConstraints;
using DungeonArchitect.Builders.GridFlow;

namespace DungeonArchitect.Builders.Grid.SpatialConstraints
{
    public class SpatialConstraintProcessorGridFlow3D : SpatialConstraintProcessor
    {
        public override SpatialConstraintRuleDomain GetDomain(SpatialConstraintProcessorContext context)
        {
            var gridConfig = context.config as GridFlowDungeonConfig;

            var domain = base.GetDomain(context);
            domain.gridSize = gridConfig.gridSize;
            return domain;
        }
    }
}
