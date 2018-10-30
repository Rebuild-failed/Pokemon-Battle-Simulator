using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RDUI
{
    public class UIManager : MonoBehaviour
    {

        public static UIManager instance = null;
        private Dictionary<string, BasePage> pages;//缓存Page,根据场景加载:{"页面名"，"页面对象"}
        //不同层级对应父对象
        private Transform canvas;
        private Transform uiHUD;
        private Transform uiPage;
        private Transform uiFloat;
        private Transform uiPopUp;

        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);

            pages = new Dictionary<string, BasePage>();
            DontDestroyOnLoad(gameObject);

        }
        // Use this for initialization
        void Start()
        {
            canvas = GameObject.Find("Canvas").transform;
            uiHUD = canvas.transform.Find("HUD");
            uiPage = canvas.transform.Find("Page");
            uiFloat = canvas.transform.Find("Float");
            uiPopUp = canvas.transform.Find("PopUp");
        }
        //根据场景加载Page
        public void LoadPage(string _sceneName)
        {
            pages.Clear();
            string pageStr = PageCollection.GetPages(_sceneName);
            string[] names = pageStr.Split('|');
            foreach (string s in names)
            {
                BasePage prefab = Resources.Load<BasePage>("Prefabs/UI/" + s);
                Transform parent = null;
                switch (prefab.pageType)
                {
                    case PageType.HUD: parent=uiHUD; break;
                    case PageType.Page: parent=uiPage; break;
                    case PageType.Float: parent=uiFloat; break;
                    case PageType.PopUp: parent=uiPopUp; break;
                    default: parent=uiPage; break;
                }
                BasePage page = Instantiate<BasePage>(prefab, parent);
                pages.Add(s, page);
            }
        }
        //打开Page
        public void OpenPage(string _pageName)
        {
            if (pages.ContainsKey(_pageName))
            {
                pages[_pageName].Open();
            }
        }
        //关闭Page
        public void ClosePage(string _pageName)
        {
            if (pages.ContainsKey(_pageName))
            {
                pages[_pageName].Close();
            }
        }
        //获取Page
        public BasePage GetPage(string _pageName)
        {
            if (pages.ContainsKey(_pageName))
            {
                return pages[_pageName];
            }
            else
            {
                return null;
            }
        }
    }
}

