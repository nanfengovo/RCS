using System.Collections.Generic;
using Volo.Abp.Domain.Values;
using System;

namespace Wms.Locations
{
    /// <summary>
    /// 三维坐标 - 值对象
    /// </summary>
    public class LocationCoordinate : ValueObject
    {
        //行
        public int Row { get; private set; }

        //列
        public int Column { get; private set; }

        //层
        public int Layer { get; private set; }

        //Efcore 需要一个无参构造函数
        private LocationCoordinate() { }

        public LocationCoordinate(int row, int column, int layer)
        {
            if (row < 0 || column < 0 || layer < 0)
            {

                throw new ArgumentException("行、列、层坐标必须为非负整数");
            }
            Row = row;
            Column = column;
            Layer = layer;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Row;
            yield return Column;
            yield return Layer;
        }
    }
}