using System.Collections.Generic;

using UnityEngine;

using Meta.Data.Params;

namespace Meta.Data
{
    /**
    * <summary>
    * Sebuah Script Berisikan Fungsi Untuk Memanggil Data Dari Sebuah Objek
    * Editor By : Izzan A.F.
    * </summary>
    */
    public class Metadata : MonoBehaviour
    {
        [SerializeField] List<MetaParam> _params;

        /**
        * <summary>
        * Mendapatkan Parameter Dari Data Yang Di-inginkan
        * </summary>
        * <returns>MetaParam</returns>
        * <param name="parameter">Paramater Yang Telah Dipilih</param>
        */
        public MetaParam FindParam(string parameter)
        {
            var param = _params.Find(x => x.id == parameter);
            if (param == null) return null;

            return param;
        }

        /**
        * <summary>
        * Mendapatkan Component Dari Parameter Yang Di-inginkan
        * </summary>
        * <returns>Component</returns>
        * <param name="parameter">Paramater Yang Telah Dipilih</param>
        */
        public Component FindParamComponent<Component>(string parameter) => FindParam(parameter).parameter.GetComponent<Component>();

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void RemoveParam(string parameter)
        {
            var param = FindParam(parameter);
            if (param == null) return;

            _params.Remove(param);
        }

        public void DeleteParam(string parameter)
        {
            var param = FindParam(parameter);
            if (param == null) return;

            Destroy(param.parameter);
            _params.Remove(param);
        }

        public void ShowParam(string parameter)
        {
            var param = FindParam(parameter);
            if (param == null) return;

            param.parameter.SetActive(true);
        }
        
        public void HideParam(string parameter)
        {
            var param = FindParam(parameter);
            if (param == null) return;

            param.parameter.SetActive(false);
        }
    }
}