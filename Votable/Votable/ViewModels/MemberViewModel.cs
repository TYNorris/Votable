using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Votable.Models;

namespace Votable.ViewModels
{
    public class MemberViewModel : BaseViewModel
    {
        #region PublicProperties
        public string FullName => _Member.FirstName + " " + _Member.LastName;

        public string Party => _Member.Party;
        #endregion

        #region Private Properties
        private Member _Member {get;set;}

        #endregion

        #region Ctor

        public MemberViewModel(Member mem)
        {
            _Member = mem;
        }
        #endregion
    }
}
