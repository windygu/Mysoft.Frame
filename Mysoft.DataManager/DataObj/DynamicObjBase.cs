using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Mysoft.DataManager
{
    /// <summary>
    /// 动态对象基类
    /// </summary>
    public class DynamicObjBase : DynamicObject
    {
        private dynamic _thisAsDynamic;
        public dynamic AsDynamic { get { return _thisAsDynamic ?? (_thisAsDynamic = (dynamic)this); } }

        public string Id { get { return (string)GetProperty(nameof(Id)); } set { SetProperty(nameof(Id), value); } }

        public string ClsId { get { return (string)GetProperty(nameof(ClsId)); } set { SetProperty(nameof(ClsId), value); } }
        public DynamicObjBase() {
            AddProperty(nameof(Id));
            AddProperty(nameof(ClsId));
            OnPropertyChanged += PropertyChanged;
            OnPropertyChanging += PropertyChanging;

        }
        public DynamicObjBase(Dictionary<string, object> properties) : this()
        {
            InitProperties(properties);
        }
        Dictionary<string, object> propertyDic = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            string name = binder.Name;
            return propertyDic.TryGetValue(name, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            var name = binder.Name;
            return SetProperty(name, value, false);
        }

        public object GetProperty(string name)
        {
            return propertyDic[name];
        }
        public bool SetProperty(string name, object value, bool isThrowWhileNotExist = true)
        {
            if (propertyDic.ContainsKey(name))
            {
                _propertyChanging(this, new PropertyChangeEventArgs(name, value));
                propertyDic[name] = value;
                _propertyChanged(this, new PropertyChangeEventArgs(name, value));
                return true;
            }
            else
            {
                if (isThrowWhileNotExist)
                    throw new Exception("属性不存在");
                return false;
            }

        }
        public void AddProperty(string name, object value = null)
        {
            propertyDic[name] = value;
        }

        public void InitProperties(IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                AddProperty(key, null);
            }
        }
        public void InitProperties(IEnumerable<KeyValuePair<string, object>> properties)
        {
            foreach (var p in properties)
            {
                AddProperty(p.Key, p.Value);
            }
        }

        public void SetProperties(IDictionary<string, object> properties)
        {
            foreach (var p in properties)
            {
                SetProperty(p.Key, p.Value, false);
            }
        }


        #region 属性改变事件
        private event PropertyChangeEventHandler _propertyChanging ;
        private int _propertyChangingEventCount;
        public event PropertyChangeEventHandler OnPropertyChanging
        {
            add
            {
                _propertyChanging += value;
                _propertyChangingEventCount++;
            }
            remove
            {
                _propertyChanging -= value;
                _propertyChangingEventCount--;
            }
        }

        private event PropertyChangeEventHandler _propertyChanged;
        private int _propertyChangedEventCount;
        public event PropertyChangeEventHandler OnPropertyChanged
        {
            add
            {
                _propertyChanged += value;
                _propertyChangedEventCount++;
            }
            remove
            {
                _propertyChanged -= value;
                _propertyChangedEventCount--;
            }
        }
        public delegate void PropertyChangeEventHandler(DynamicObjBase sender, PropertyChangeEventArgs e);

        protected virtual void PropertyChanging(DynamicObjBase sender, PropertyChangeEventArgs e)
        {

        }
        protected virtual void PropertyChanged(DynamicObjBase sender, PropertyChangeEventArgs e)
        {

        }
        public class PropertyChangeEventArgs : EventArgs
        {
            public PropertyChangeEventArgs(string propertyName, object value)
            {
                PropertyName = propertyName;
                Value = value;
            }
            public string PropertyName { get; set; }

            public object Value { get; set; }
        }
        #endregion


    }
}
