using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Mvvm.Base
{
    public static class Status_HasChangesEx
    {
        /// <summary> КОСТЫЛЬ!
        ///  При загрузке из базы все объекты помечаются что имеют изменения. Чтобы этого не происходило, отсвлеживаю флаг. 
        ///  При загрузке базы выставляю IsWorking = false, после загрузки базы и уже работе системы IsWorking = true.
        /// </summary>
        public static bool IsWorking = false;
    }

    public abstract class Entity : ValidatableModel
    {
        private bool m_HasChanges;
        private readonly HashSet<string> m_Changes;

        protected Entity() : base()
        {
            m_Changes = new HashSet<string>();
        }

        /// <summary> Индикатор о наличии изменений данных в объекте, и объект ещё не сохранён </summary>
        [NotMapped] public virtual bool HasChanges
        {
            get { return m_HasChanges; }
            private set { SetProperty(ref m_HasChanges, value); }
        }

        /// <summary> Взводит флаг произошли изменения объекта </summary>
        /// <param name="_propertyName"> Наименование поля, где произошли изменения </param>
        public virtual void SetChange(string _propertyName)
        {
            HasChanges = true;
            m_Changes.Add(_propertyName);
        }

        public IReadOnlyCollection<string> GetChanges() => m_Changes.ToArray();
        public void ClearChanges()
        {
            HasChanges = false;
            m_Changes.Clear();
        }

        protected bool SetPropertyAndTrackChanges<T>(ref T _field, T _value, [CallerMemberName] string _propertyName = null)
        {
            if (SetPropertyAndValidate(ref _field, _value, _propertyName)) //if (SetProperty(ref _field, _value, _propertyName))
            {
                if (Status_HasChangesEx.IsWorking) HasChanges = true;

                m_Changes.Add(_propertyName);
                return true;
            }

            return false;
        }
    } // --- Entity : class ---
}