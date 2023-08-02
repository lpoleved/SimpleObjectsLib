using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable

namespace Simple.Objects
{
    #region |   Code Generated by Simple.Objects Code Generator   |

    partial class SimpleObject
    {
        #region |   One-To-Many Relation Properties   |

        /// <summary>
        /// Gets one-to-many relation foreign GraphElements collection.
        /// </summary>
        public virtual SimpleObjectCollection<GraphElement> GraphElements
        {
            get { return this.GetOneToManyForeignObjectCollection<GraphElement>(RelationPolicyModelBase.OneToManyGraphElementToSimpleObject); }
        }

        #endregion |   One-To-Many Relation Properties   |
    }

    #endregion |   Code Generated by Simple.Objects Code Generator   |
}
