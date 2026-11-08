using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


//该类的作用是用PlayerPrefs存储和读取自定义类的信息
public class PlayerPrefsDataMgr 
{
    /// <summary>
    /// 单例模式：保证整个程序仅有该类的1个实例，提供全局访问入口
    /// 三要素：静态字段保存唯一实例、只读静态属性对外访问、私有构造禁止外部new
    /// </summary>
 
    //静态字段
    private static PlayerPrefsDataMgr instance = new PlayerPrefsDataMgr();

    //全局唯一访问入口
    public static PlayerPrefsDataMgr Instance
    {
        get
        {
            return instance;
        }
    }

    //禁止外部使用构造
    private PlayerPrefsDataMgr()
    {

    }


    //存
    public void SaveData(object data, string KeyName)
    {
        //第一步：得到类中所有的数据类型
        Type dataType = data.GetType();
        FieldInfo[] infos = dataType.GetFields();
        //第二步：定义我们自己的key命名规则
        //这里我们规则是 KeyName_类的名字_类属性的类型(如int)_类属性的名字(如age)
        FieldInfo info;
        string saveKeyName = "";
        for(int i = 0; i < infos.Length; i++)
        {
            info = infos[i];
            saveKeyName = KeyName + "_" + dataType.Name + "_" + info.FieldType.Name  + "_" + info.Name;
            //第四步 存储值和键，这里封装一个方法saveValue来存
            SaveValue(info.GetValue(data), saveKeyName);
        }
        PlayerPrefs.Save();
    }
    private void SaveValue(object value, string KeyName)
    {
        Type  fieldType = value.GetType();
        if(fieldType == typeof(string))
        {
            PlayerPrefs.SetString(KeyName, (string)value);
        }
        else if (fieldType == typeof(int))
        {
            PlayerPrefs.SetInt(KeyName, (int)value);
        }
        else if (fieldType == typeof(float))
        {
            PlayerPrefs.SetFloat(KeyName, (float)value);
        }
        else if (fieldType == typeof(bool))
        {
            PlayerPrefs.SetInt(KeyName,  (bool)value ? 1 : 0);
        }
        //存List数组
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            IList list = value as IList;
            PlayerPrefs.SetInt(KeyName, list.Count);
            int index = 0;
            foreach(object obj in list)
            {
                SaveValue(obj, KeyName + index);
                ++index;
            }
        }
        //存Dictionary数组
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            IDictionary dictionary = value as IDictionary;
            PlayerPrefs.SetInt(KeyName, dictionary.Count);
            int index = 0;
            foreach(object key in dictionary.Keys)
            {
                SaveValue(key, KeyName + "_key_" + index);
                SaveValue(dictionary[key], KeyName + "_value_" + index);
                ++index;
            }
        }
        else
        {
            SaveData(value, KeyName);
        }
    }


    //读
    public object LoadData(Type type, string KeyName)
    {
        object data = Activator.CreateInstance(type);
        FieldInfo[] infos = type.GetFields();
        FieldInfo info;
        string LoadKeyName = "";
        for(int i  = 0; i < infos.Length; i++)
        {
            info = infos[i];
            LoadKeyName = KeyName + "_" + type.Name + "_" + info.FieldType.Name + "_" + info.Name;
            info.SetValue(data, LoadValue(info.FieldType, LoadKeyName));
        }    
        return data;
    }

    public object LoadValue(Type fieldType,string LoadKeyName)
    {
        if (fieldType == typeof(string))
        {
            return PlayerPrefs.GetString(LoadKeyName, "");
        }
        else if (fieldType == typeof(int))
        {
            return PlayerPrefs.GetInt(LoadKeyName, 0);
        }
        else if (fieldType == typeof(float))
        {
            return PlayerPrefs.GetFloat(LoadKeyName, 0);
        }
        else if (fieldType == typeof(bool))
        {
            return PlayerPrefs.GetInt(LoadKeyName, 0) == 1 ? true : false;
        }
        //读List
        else if (typeof(IList).IsAssignableFrom(fieldType))
        {
            IList list = Activator.CreateInstance(fieldType) as IList;
            int count = PlayerPrefs.GetInt(LoadKeyName, 0);
            for(int i = 0;i < count;i++)
            {
                list.Add(LoadValue(fieldType.GetGenericArguments()[0], LoadKeyName + i));
            }
            return list;
        }
        //读Dictionary
        else if (typeof(IDictionary).IsAssignableFrom(fieldType))
        {
            IDictionary dict = Activator.CreateInstance(fieldType) as IDictionary;
            int count = PlayerPrefs.GetInt(LoadKeyName, 0);
            Type[] kvType = fieldType.GetGenericArguments();
            for (int i = 0; i < count; i++)
            {
                dict.Add(LoadValue(kvType[0], LoadKeyName + "_key_" + i), LoadValue(kvType[1], LoadKeyName + "_value_" + i));
            }
            return dict;
        }
        else
        {
            
            return LoadData(fieldType, LoadKeyName);
            
        }

    }
}
