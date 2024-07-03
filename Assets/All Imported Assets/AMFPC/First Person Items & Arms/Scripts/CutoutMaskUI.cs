using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace All_Imported_Assets.AMFPC.First_Person_Items___Arms.Scripts
{
    public class CutoutMaskUI : Image
    {
        public override Material materialForRendering 
        {
            get 
            {  
                Material material =  new Material( base.materialForRendering);
                material.SetInt("_StencilComp", (int)CompareFunction.NotEqual);
                return material;
            }
        
        }
    }
}
