using System.Collections.Generic;

using UnityEngine;

using Meta.Data.Params;

namespace Meta.Data
{
    /**
    * <summary>
    * A script to make it easier to get the child of the object
    * <br> Created By : Izzan A. Fu'ad. </br>
    * </summary>
    */
    public class Metadata : MonoBehaviour
    {
        [SerializeField] List<MetaParam> _params;

        /**
        * <summary>
        * Menambahkan Parameter Parameter Baru
        * </summary>
        * <param name="id"> Id Dari Parameter Yang Ingin Ditambahkan </param>
        * <param name="parameter"> Paramater Yang Ingin Dimasukan </param>
        */
        public void AddParam(string id, GameObject parameter)
        {
            MetaParam newParam = new()
            {
                id = id,
                parameter = parameter
            };

            AddParam(newParam);
        }

        /**
        * <summary>
        * Menambahkan Parameter Parameter Baru
        * </summary>
        * <param name="parameter"> Paramater Yang Ingin Dimasukan </param>
        */
        public void AddParam(MetaParam parameter)
        {
            _params.Add(parameter);
        }

        /**
        * <summary>
        * Mendapatkan Parameter Dari Data Yang Di-inginkan
        * </summary>
        * <returns>MetaParam</returns>
        * <param name="parameter">Paramater Yang Telah Dipilih</param>
        */
        public MetaParam FindParam(string parameter)
        {
            MetaParam param = _params.Find(x => x.id == parameter);
            if (param == null)
                return null;

            return param;
        }

        /**
        * <summary>
        * Mendapatkan Component Dari Parameter Yang Di-inginkan
        * </summary>
        * <returns>Component</returns>
        * <param name="parameter">Paramater Yang Telah Dipilih</param>
        */
        public Component FindParamComponent<Component>(string parameter)
        {
            return FindParam(parameter).parameter.GetComponent<Component>();
        }

        /**
        * <summary>
        * Memperlihatkan GameObject Yang Terhubung Oleh MetaData Ini.
        * </summary>
        */
        public void Show()
        {
            gameObject.SetActive(true);
        }

        /**
        * <summary>
        * Menyembunyikan GameObject Yang Terhubung Oleh MetaData Ini.
        * </summary>
        */
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        /**
        * <summary>
        * Menghapus Parameter Dari List
        * </summary>
        * <param name="parameter">Paramater Yang Telah Dipilih</param>
        */
        public void RemoveParam(string parameter)
        {
            var param = FindParam(parameter);
            if (param == null) return;

            _params.Remove(param);
        }

        /**
        * <summary>
        * Menghancurkan Parameter Sekaligus Menghapus Parameter Dari List
        * </summary>
        * <param name="parameter">Paramater Yang Telah Dipilih</param>
        */
        public void DeleteParam(string parameter)
        {
            var param = FindParam(parameter);
            if (param == null) return;

            _params.Remove(param);
            Destroy(param.parameter);
        }

        /**
        * <summary>
        * Memperlihatkan Parameter Yang Di-inginkan
        * </summary>
        * <param name="parameter">Paramater Yang Telah Dipilih</param>
        */
        public void ShowParam(string parameter)
        {
            var param = FindParam(parameter);
            if (param == null) return;

            param.parameter.SetActive(true);
        }

        /**
        * <summary>
        * Menyembunyikan Parameter Yang Di-inginkan
        * </summary>
        * <param name="parameter">Paramater Yang Telah Dipilih</param>
        */
        public void HideParam(string parameter)
        {
            var param = FindParam(parameter);
            if (param == null) return;

            param.parameter.SetActive(false);
        }
    }
}