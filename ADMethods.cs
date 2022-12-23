using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.DirectoryServices;


namespace AMAT.BIM_UMSWeb
{
        public class ADMethods
        {
            string sLDAP_Path = System.Configuration.ConfigurationManager.AppSettings["LDAP_Path"];

            #region GetEmployeeAttributes
            /// <summary>
            /// Enum AD properties
            /// </summary>
            public enum AdPrpoertyParameters
            {
                employeeid,
                sAMAccountName,
                displayName,
                mail
            }
            /// <summary>
            /// 
            /// </summary>
            public enum PropertyBagLDAPKeys
            {
                AmatLdapPath,
                TelLdapPath
            }

            private const string CONST_REGULARFULLTIME = "R";
            private const string CONST_CONTRACTUSER = "C";
            private const string CONST_SUPPLIERDIRECT = "T";
            private const string CONST_TRUSER = "TR";

            private const string CONST_AMAT = "AMAT";
            private const string CONST_TEL = "TEL";

            /// <summary>
            /// All active directory properties 
            /// </summary>
            public class ADAttributes
            {
                public string employeeId { get; set; }
                public string employeeType { get; set; }
                public string mail { get; set; }
                public string sAMAccountName { get; set; }
                public string displayName { get; set; }
                public string givenName { get; set; }
                public string manager { get; set; }
                public string memberOf { get; set; }
                public string department { get; set; }
                public string departmentNumber { get; set; }
                public string division { get; set; }
                public string userAccountControl { get; set; }
                public string telephoneNumber { get; set; }
                public string postOfficeBox { get; set; }
                public string PhysicalDeliveryOfficeName { get; set; }
                public string objectClass { get; set; }
                public string uId { get; set; }
                public string domain { get; set; }
                public string Location { get; set; }
                public string Company { get; set; }
                public string FirstName { get; set; }
                public string LastName { get; set; }
                //    public string ManagerEmailAddress { get; set; }
                public string managerSamAcnt { get; set; }
                public string ManagerEmail { get; set; }
                public string ManagerLoginName { get; set; }
                public string ManagerDisplayName { get; set; }
                public string ManagerDomain { get; set; }
                public string co { get; set; }
                public string ManagerEmployeeID { get; set; }

            }

            /// <summary>
            /// To get  all Ad properties
            /// </summary>
            /// <param name="directoryEntry">Domain directory</param>
            /// <param name="Filtervalue">Ad filterValue</param>
            /// <param name="Input">Loginname\EmailId\EmpId</param>
            /// <returns></returns>
            private static ADAttributes GetDirectoryAttributes(SearchResult sresult, string Filtervalue, string Input, string DomainName)
            {


                if (sresult != null)
                {
                    ADAttributes objAd = new ADAttributes
                    {
                        employeeId = sresult.Properties["employeeid"].Count > 0 ? sresult.Properties["employeeid"][0].ToString() : string.Empty,
                        employeeType = sresult.Properties["employeeType"].Count > 0 ? sresult.Properties["employeeType"][0].ToString() : string.Empty,
                        sAMAccountName = sresult.Properties["sAMAccountName"].Count > 0 ? sresult.Properties["sAMAccountName"][0].ToString() : string.Empty,
                        department = sresult.Properties["department"].Count > 0 ? sresult.Properties["department"][0].ToString() : string.Empty,
                        departmentNumber = sresult.Properties["departmentNumber"].Count > 0 ? sresult.Properties["departmentNumber"][0].ToString() : string.Empty,
                        division = sresult.Properties["division"].Count > 0 ? sresult.Properties["division"][0].ToString() : string.Empty,
                        manager = sresult.Properties["manager"].Count > 0 ? sresult.Properties["manager"][0].ToString() : string.Empty,
                        managerSamAcnt = (sresult.Properties.Contains("manager") == true) ? Convert.ToString(sresult.Properties["manager"][0]).Split(',')[0].Split('=')[1] : string.Empty, // Splitting manger name
                        //manager = (sresult.Properties.Contains("manager") == true) ? Convert.ToString(sresult.Properties["manager"][0]).Split(',')[0].Split('=')[1] : string.Empty, // Splitting manger name
                        displayName = sresult.Properties["displayName"].Count > 0 ? sresult.Properties["displayName"][0].ToString() : string.Empty,
                        PhysicalDeliveryOfficeName = sresult.Properties["physicalDeliveryOfficeName"].Count > 0 ? sresult.Properties["physicalDeliveryOfficeName"][0].ToString() : string.Empty,
                        mail = sresult.Properties["mail"].Count > 0 ? sresult.Properties["mail"][0].ToString() : string.Empty,
                        memberOf = sresult.Properties["memberOf"].Count > 0 ? sresult.Properties["memberOf"][0].ToString() : string.Empty,
                        //memberOf = (sresult.Properties["memberOf"].Count > 0) ? Convert.ToString(sresult.Properties["memberOf"][0]).Split(',')[0].Split('=')[1] : string.Empty, //Splitting manger name
                        userAccountControl = sresult.Properties["userAccountControl"].Count > 0 ? sresult.Properties["userAccountControl"][0].ToString() : string.Empty,
                        givenName = sresult.Properties["givenName"].Count > 0 ? sresult.Properties["givenName"][0].ToString() : string.Empty,
                        objectClass = sresult.Properties["objectClass"].Count > 0 ? sresult.Properties["objectClass"][0].ToString() : string.Empty,
                        postOfficeBox = sresult.Properties["postOfficeBox"].Count > 0 ? sresult.Properties["postOfficeBox"][0].ToString() : string.Empty,
                        telephoneNumber = sresult.Properties["telephoneNumber"].Count > 0 ? sresult.Properties["telephoneNumber"][0].ToString() : string.Empty,
                        uId = sresult.Properties["uId"].Count > 0 ? sresult.Properties["uId"][0].ToString() : string.Empty,
                        Location = sresult.Properties["L"].Count > 0 ? sresult.Properties["L"][0].ToString() : string.Empty,
                        Company = sresult.Properties["Company"].Count > 0 ? sresult.Properties["Company"][0].ToString() : string.Empty,
                        FirstName = sresult.Properties["givenName"].Count > 0 ? sresult.Properties["givenName"][0].ToString() : string.Empty,
                        LastName = sresult.Properties["sn"].Count > 0 ? sresult.Properties["sn"][0].ToString() : string.Empty,
                        //   ManagerEmailAddress = sresult.Properties["ManagerEmailAddress"].Count > 0 ? sresult.Properties["ManagerEmailAddress"][0].ToString() : string.Empty,
                        co = sresult.Properties["co"].Count > 0 ? sresult.Properties["co"][0].ToString() : string.Empty,
                        domain = DomainName
                    };
                    return objAd;
                }
                else
                {
                    return null;
                }
            }
            /// <summary>
            /// To get Employee Attributes
            /// </summary>
            /// <param name="Input">Login Name/empid/mailI</param>
            /// <param name="PropertyParameter"></param>
            /// <returns></returns>
            public ADAttributes GetEmployeeAttributes(string Input, AdPrpoertyParameters PropertyParameter)
            {
                ADAttributes attributes = null;
                try
                {
                    string strLDAPPath = System.Configuration.ConfigurationManager.AppSettings["LDAP_Path"];

                    attributes = DirectoryEntryforAttributes(strLDAPPath, PropertyParameter.ToString(), Input, CONST_AMAT);
                    //if (attributes == null)
                    //{
                    //    attributes = DirectoryEntryforAttributes(strLDAPPath, PropertyParameter.ToString(), Input, CONST_TEL);
                    //}


                    ADAttributes mgrAttributes = null;
                    mgrAttributes = DirectoryEntryforAttributes(strLDAPPath, AdPrpoertyParameters.sAMAccountName.ToString(), attributes.managerSamAcnt, CONST_AMAT);
                    //if (mgrAttributes == null)
                    //{
                    //    mgrAttributes = DirectoryEntryforAttributes(strLDAPPath, AdPrpoertyParameters.sAMAccountName.ToString(), attributes.managerSamAcnt, CONST_TEL);
                    //}
                    if (mgrAttributes != null)
                    {
                        attributes.ManagerEmail = mgrAttributes.mail;
                        attributes.ManagerLoginName = mgrAttributes.sAMAccountName;
                        attributes.ManagerDisplayName = mgrAttributes.displayName;
                        attributes.ManagerDomain = mgrAttributes.domain;
                        attributes.ManagerEmployeeID = mgrAttributes.employeeId;

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return attributes;
            }

            #endregion

            #region IsTemporaryUser
            /// <summary>
            /// This method used for Employee is Temporary user or not
            /// </summary>
            public bool IsTemporaryUser(string Input, AdPrpoertyParameters PropertyParameter)
            {
                string employeeType = string.Empty;
                employeeType = GetEmployeeType(Input, PropertyParameter);
                return employeeType == "T";
            }
            #endregion

            #region IsContractUser
            /// <summary>
            /// To check the Employee is Contract or not
            /// </summary>
            public bool IsContractUser(string Input, AdPrpoertyParameters PropertyParameter)
            {
                string employeeType = string.Empty;
                employeeType = GetEmployeeType(Input, PropertyParameter);
                return employeeType == CONST_CONTRACTUSER;

            }
            #endregion

            #region IsSuppilerDirect
            /// <summary>
            /// To Check the Employee is Supplier direct or not
            /// </summary>
            public bool IsSuppilerDirect(string Input, AdPrpoertyParameters PropertyParameter)
            {
                string employeeType = string.Empty;
                employeeType = GetEmployeeType(Input, PropertyParameter);
                return employeeType == CONST_SUPPLIERDIRECT;

            }
            #endregion

            #region IsRegularFullTime
            /// <summary>
            /// To Check the Employee isRegular FullTime or not
            /// </summary>
            /// <param name="Input">Login Name/empid/mailID</param>
            /// <param name="PropertyParameter"></param>
            /// <returns></returns>
            public bool IsRegularfullTime(string Input, AdPrpoertyParameters PropertyParameter)
            {
                string employeeType = string.Empty;
                employeeType = GetEmployeeType(Input, PropertyParameter);
                return employeeType == CONST_REGULARFULLTIME;
            }
            #endregion

            #region IsTRUser
            /// <summary>
            /// This method used for the current user is TR or not
            /// </summary>
            /// <param name="Input"></param>
            /// <param name="PropertyParameter"></param>
            /// <returns></returns>
            public bool IsTRUser(string Input, AdPrpoertyParameters PropertyParameter)
            {
                string displayName = string.Empty;
                bool isTRUser = false;
                ADAttributes objAdattributes;
                objAdattributes = GetEmployeeAttributes(Input, PropertyParameter);
                if (objAdattributes != null)
                {
                    displayName = objAdattributes.displayName;
                    if (displayName.IndexOf("--" + CONST_TRUSER) != -1)
                    {
                        isTRUser = true;
                    }
                }
                return isTRUser;

            }
            #endregion

            #region GetEmployeeType
            /// <summary>
            /// To get Employee type for used is contract or Tempray or TE used
            /// </summary>
            public string GetEmployeeType(string Input, AdPrpoertyParameters PropertyParameter)
            {
                string employeeType = string.Empty;
                ADAttributes objAdattributes;
                objAdattributes = GetEmployeeAttributes(Input, PropertyParameter);
                if (objAdattributes != null)
                {
                    employeeType = objAdattributes.employeeType;
                }

                return employeeType;
            }
            #endregion

            // ***Added by Swathi****//

            #region IsUserExist
            /// <summary>
            /// To Check this method user exist or not in active directory
            /// </summary>
            /// <param name="Input"></param>
            /// <param name="values"></param>
            /// <returns></returns>
            public bool IsUserExist(string Input, AdPrpoertyParameters PropertyParameter)
            {
                SearchResult sresult = null;
                sresult = DirectoryEntry(PropertyBagLDAPKeys.AmatLdapPath.ToString(), PropertyParameter.ToString(), Input);
                if (sresult == null)
                {
                    sresult = DirectoryEntry(PropertyBagLDAPKeys.TelLdapPath.ToString(), PropertyParameter.ToString(), Input);
                }
                return sresult != null;

            }
            #endregion

            #region AMATDirectoryEntry_ForGroup
            /// <summary>
            /// AMAT Directory Entry for Group
            /// </summary>
            /// <param name="Input"></param>
            /// <param name="values"></param>
            /// <returns></returns>
            //private static DirectorySearcher AMATDirectoryEntry_ForGroup(string GroupName)
            //{
            //    DirectorySearcher deSearch = new DirectorySearcher();
            //    DirectoryEntry de = new DirectoryEntry("LDAP://DC=amat,DC=com");
            //    deSearch.SearchRoot = de;
            //    deSearch.Filter = "(&(objectClass=group)(cn=" + GroupName + "))";

            //    return deSearch;
            //}
            //#endregion

            //#region TELTDirectoryEntry_ForGroup
            ///// <summary>
            ///// TEL Directory Entry for Group
            ///// </summary>
            ///// <param name="Input"></param>
            ///// <param name="values"></param>
            ///// <returns></returns>
            //private static DirectorySearcher TELTDirectoryEntry_ForGroup(string GroupName)
            //{
            //    DirectorySearcher deSearch = new DirectorySearcher();
            //    DirectoryEntry de = new DirectoryEntry("LDAP://DC=amat,DC=com");
            //    deSearch.SearchRoot = de;
            //    deSearch.Filter = "(&(objectClass=group)(cn=" + GroupName + "))";

            //    return deSearch;
            //}
            //#endregion

            #region GetUsersForGroup
            /// <summary>
            /// To get the people in the given AD Group from the two domans
            /// </summary>
            /// <param name="GroupName">AD group name</param>
            /// <returns>returs array list of all the users in the given AD goup</returns>
            public ArrayList GetUsersForGroup(string GroupName)
            {
                ArrayList alluserEmailIDs = new ArrayList();
                try
                {

                    alluserEmailIDs = AMATGetUsersForGroup(GroupName);

                    alluserEmailIDs = TELGetUsersForGroup(GroupName, alluserEmailIDs);
                }
                catch
                {


                }

                return alluserEmailIDs;
            }
            #endregion

            /// <summary>
            /// To get the people in the given AD Group from AMAT Domain
            /// </summary>
            /// <param name="GroupName">AD group name</param>
            /// <returns>returs array list of all the users in the given AD goup</returns>
            public ArrayList AMATGetUsersForGroup(string GroupName)
            {
                SearchResult results;
                SearchResultCollection resulll;
                ArrayList alEmailID = new ArrayList();

                DirectorySearcher deSearch = DirectoryEntry_ForGroup(PropertyBagLDAPKeys.AmatLdapPath.ToString(), GroupName);
                results = deSearch.FindOne();
                resulll = deSearch.FindAll();

                if (resulll != null)
                {
                    DirectoryEntry deGroup = new DirectoryEntry(results.Path);
                    System.DirectoryServices.PropertyCollection pcoll = deGroup.Properties;
                    int n = pcoll["member"].Count;

                    string strMail = null;
                    for (int l = 0; l < n; l++)
                    {
                        DirectoryEntry deUser = new DirectoryEntry("LDAP://amat.com/" + pcoll["member"][l].ToString());
                        strMail = GetProperty(deUser, "mail") + ",";
                        if (null != strMail && strMail.Trim() != "")
                        {
                            alEmailID.Add(strMail);
                        }
                        deUser.Close();
                    }
                    //de.Close();
                    deGroup.Close();
                }
                return alEmailID;
            }
            # endregion

            #region TELGetUsersForGroup
            /// <summary>
            /// To get the people in the given AD Group from TEL Domain
            /// </summary>
            /// <param name="GroupName">AD group name</param>
            /// <returns>returs array list of all the users in the given AD goup</returns>
            public ArrayList TELGetUsersForGroup(string GroupName, ArrayList alEmailID)
            {
                DirectorySearcher deSearch = DirectoryEntry_ForGroup(PropertyBagLDAPKeys.TelLdapPath.ToString(), GroupName);
                SearchResult results = deSearch.FindOne();
                SearchResultCollection resulll = deSearch.FindAll();

                if (resulll != null)
                {
                    DirectoryEntry deGroup = new DirectoryEntry(results.Path);
                    System.DirectoryServices.PropertyCollection pcoll = deGroup.Properties;
                    int n = pcoll["member"].Count;

                    string strMail = null;
                    for (int l = 0; l < n; l++)
                    {
                        DirectoryEntry deUser = new DirectoryEntry("LDAP://TEL.com/" + pcoll["member"][l].ToString());
                        strMail = GetProperty(deUser, "mail") + ",";
                        if (null != strMail && strMail.Trim() != "")
                        {
                            alEmailID.Add(strMail);
                        }
                        deUser.Close();
                    }
                    //de.Close();
                    deGroup.Close();
                }
                return alEmailID;
            }
            #endregion

            #region GetProperty
            /// <summary>
            /// To get the Member of propery for the user from AD
            /// </summary>
            /// <param name="oDE"></param>
            /// <param name="PropertyName"></param>
            /// <returns></returns>
            private static string GetProperty(DirectoryEntry oDE, string PropertyName)
            {
                if (oDE.Properties.Contains(PropertyName))
                {
                    return oDE.Properties[PropertyName][0].ToString();
                }
                else
                {
                    return string.Empty;
                }
            }
            #endregion

            #region GetDomainName

            /// <summary>
            /// Directry entry for TEL Domain
            /// </summary>
            /// <param name="Input"> value on which the AD filter need to happen</param>
            /// <param name="PropertyParameter">enum value</param>
            /// <returns> returns Domain name</returns>
            public string GetDomainName(string Input, AdPrpoertyParameters PropertyParameter)
            {
                SearchResult sresult = null;
                SearchResult sresultTEL = null;

                sresult = DirectoryEntry(PropertyBagLDAPKeys.AmatLdapPath.ToString(), PropertyParameter.ToString(), Input);
                if (sresult == null)
                {
                    sresultTEL = DirectoryEntry(PropertyBagLDAPKeys.TelLdapPath.ToString(), PropertyParameter.ToString(), Input);
                }

                if (sresult != null)
                    return CONST_AMAT;
                else
                    return CONST_TEL;
            }

            #endregion

            #region GetuserMemberof_Groupinfo
            /// <summary>
            /// This method used for  Find out the given user belongs to which group in AD 
            /// </summary>
            /// <param name="username">login name</param>
            /// <returns>return  to Group name</returns>
            public string GetuserMemberof_Groupinfo(string username, ArrayList lstGroups)
            {
                string memberofgroupnames = string.Empty;
                SearchResult sresult = null;
                //string[] strUserId = username.Split('\\');
                sresult = DirectoryEntry(PropertyBagLDAPKeys.AmatLdapPath.ToString(), "sAMAccountName", username);
                //if (sresult == null)
                //{
                //    sresult = DirectoryEntry(PropertyBagLDAPKeys.TelLdapPath.ToString(), "sAMAccountName", username);
                //}
                if (sresult != null && sresult.Properties["memberOf"].Count > 0)
                {
                    //adEntry = sresult.GetDirectoryEntry();
                    //memberofgroupname = sresult.Properties["memberof"][0].ToString();
                    //memberofgroupname = memberofgroupname.Substring(0, memberofgroupname.IndexOf(','));
                    //memberofgroupname = memberofgroupname.Substring(memberofgroupname.IndexOf('=') + 1);
                    if (sresult.Properties["memberOf"].Count > 0)
                    {
                        foreach (string prop in sresult.Properties["memberOf"])
                        {
                            foreach (string strGroupName in lstGroups)
                            {
                                if (prop.ToLower().Contains("cn=" + strGroupName.ToLower()))
                                {
                                    memberofgroupnames = memberofgroupnames + "," + strGroupName;
                                }
                            }
                        }
                    }
                    //strName = adEntry.Properties["givenName"].Value.ToString() + " " + adEntry.Properties["sn"].Value.ToString();
                }
                memberofgroupnames = memberofgroupnames.TrimStart(',');
                return memberofgroupnames;
            }

            #endregion

            #region UserBelongsToTheGroups
            /// <summary>
            /// This method used for  Find out the given user belongs to the given AD group
            /// </summary>
            /// <param name="username">login name</param>
            /// <param name="lstGroups">List of AD groups</param>
            /// <returns>return T/F</returns>
            public bool UserBelongsToTheGroups(string userName, ArrayList lstGroups)
            {
                SearchResult searcher = null;
                searcher = DirectoryEntry(PropertyBagLDAPKeys.AmatLdapPath.ToString(), "sAMAccountName", userName);
                if (searcher.Properties["memberOf"].Count > 0)
                {
                    foreach (string prop in searcher.Properties["memberOf"])
                    {
                        foreach (string strGroupName in lstGroups)
                        {
                            if (prop.ToLower().Contains("cn=" + strGroupName.ToLower()))
                            {
                                return true;
                            }
                        }
                    }
                }
                //else
                //{
                //    searcher = DirectoryEntry(PropertyBagLDAPKeys.TelLdapPath.ToString(), "sAMAccountName", userName);
                //    if (searcher.Properties["memberOf"].Count > 0)
                //    {
                //        foreach (string prop in searcher.Properties["memberOf"])
                //        {
                //            foreach (string strGroupName in lstGroups)
                //            {
                //                if (prop.ToLower().Contains("cn=" + strGroupName.ToLower()))
                //                {
                //                    return true;
                //                }
                //            }
                //        }
                //    }
                //}
                return false;
            }
            #endregion

            #region GetEmployeeAttributes
            /// <summary>
            /// To get Employee Attributes overloading method 
            /// </summary>
            /// <param name="Input">Login Name/empid/mailI</param>
            /// <param name="PropertyParameter">type of input value(enum values) </param>
            /// <param name="currentuserlogin"></param>
            /// <returns>Returns the AD attributes for the give  user of ADAttributes class trpe</returns>
            public ADAttributes GetEmployeeAttributes(string Input, AdPrpoertyParameters PropertyParameter, string currentuserlogin)
            {
                ADAttributes attributes = null;
                string domainName = currentuserlogin.Split(new char[] { '\\' }, System.StringSplitOptions.RemoveEmptyEntries)[0];
                switch (domainName.ToUpper())
                {
                    case CONST_AMAT:
                        {
                            attributes = DirectoryEntryforAttributes(PropertyBagLDAPKeys.AmatLdapPath.ToString(), PropertyParameter.ToString(), Input, domainName.ToUpper());
                        }
                        break;
                    case CONST_TEL:
                        {
                            attributes = DirectoryEntryforAttributes(PropertyBagLDAPKeys.TelLdapPath.ToString(), PropertyParameter.ToString(), Input, domainName.ToUpper());
                        }
                        break;
                }
                return attributes;

            }
            #endregion

            #region DirectoryEntryforAttributes
            /// <summary>
            /// Method To get the user Attributes from the AD
            /// </summary>
            /// <param name="Input">Login Name/empid/mailI</param>
            /// <param name="Filtervalue">enum type for input value</param>
            /// <param name="PropertyKey">PropertyKey to get LDAp path from Property bag</param>
            /// <param name="DomainName">Domain</param>
            /// <returns></returns>
            public ADAttributes DirectoryEntryforAttributes(string PropertyKey,string Filtervalue, string Input, string DomainName)
            {
                SearchResult sresult;
                string query = string.Empty;
                string ldappath = sLDAP_Path;
                DirectoryEntry entry_Amat = new DirectoryEntry(ldappath);
                DirectorySearcher userSearch_Amat = new DirectorySearcher(entry_Amat);
                query = string.Format("(&(objectClass=user)(" + Filtervalue + "={0}))", Input);
                userSearch_Amat.Filter = query;
                //Remove this code if not required
                sresult = userSearch_Amat.FindOne();
                if (sresult == null)
                {
                    query = string.Format("(&(objectClass=contact)(" + Filtervalue + "={0}))", Input);
                    userSearch_Amat.Filter = query;
                    //samSearcher.PropertiesToLoad.Add(Filtervalue);
                    sresult = userSearch_Amat.FindOne();

                }
                ADAttributes objAd = GetDirectoryAttributes(sresult, Filtervalue, Input, DomainName);
                return objAd;

            }

            #endregion

            #region DirectoryEntry

            /// <summary>
            /// To get results form the AD
            /// </summary>
            /// <param name="Input">Login Name/empid/mailI</param>
            /// <param name="Filtervalue">enum type for input value</param>
            /// <param name="PropertyKey">PropertyKey to get LDAp path from Property bag</param>
            /// <returns></returns>
            private SearchResult DirectoryEntry(string PropertyKey, string Filtervalue, string Input)
            {
                SearchResult sresult;
                string query = string.Empty;
                string ldappath = sLDAP_Path;
                DirectoryEntry entry_Amat = new DirectoryEntry(ldappath);
                DirectorySearcher userSearch_Amat = new DirectorySearcher(entry_Amat);
                query = string.Format("(&(objectClass=user)(" + Filtervalue + "={0}))", Input);
                userSearch_Amat.Filter = query;
                //Remove this code if not required
                sresult = userSearch_Amat.FindOne();
                if (sresult == null)
                {
                    query = string.Format("(&(objectClass=contact)(" + Filtervalue + "={0}))", Input);
                    userSearch_Amat.Filter = query;
                    //Remove this code if not required
                    sresult = userSearch_Amat.FindOne();
                }

                return sresult;
            }
            #endregion

            #region DirectoryEntry_ForGroup
            /// <summary>
            /// To get people from the AD for the given group
            /// </summary>
            /// <param name="PropertyKey">PropertyKey to get LDAp path from Property bag</param>
            /// <param name="GroupName">AD group name</param>
            /// <returns></returns>
            private DirectorySearcher DirectoryEntry_ForGroup(string PropertyKey, string GroupName)
            {
                string ldappath = sLDAP_Path;
                DirectorySearcher deSearch = new DirectorySearcher();
                DirectoryEntry de = new DirectoryEntry(ldappath);
                deSearch.SearchRoot = de;
                deSearch.Filter = "(&(objectClass=group)(cn=" + GroupName + "))";

                return deSearch;
            }
            #endregion

            #region GetDomainName
            /// <summary>
            /// To get the domai name for the given input
            /// </summary>
            /// <param name="currentuserlogin">Login Name with domain</param>
            /// <returns></returns>
            public string GetDomainName(string currentuserlogin)
            {
                string domainName = currentuserlogin.Split(new char[] { '\\' }, System.StringSplitOptions.RemoveEmptyEntries)[0];
                return domainName.ToUpper();
            }
            #endregion


            public void GetGroupMembers(string strGroup)
            {
                System.Collections.Generic.List<string> groupMemebers = new System.Collections.Generic.List<string>();

                try
                {
                    DirectoryEntry ent = new DirectoryEntry("LDAP://DC=AMAT,DC=COM");// Change by your AD link

                    DirectorySearcher srch = new DirectorySearcher("(CN=" + strGroup + ")");

                    SearchResultCollection coll = srch.FindAll();

                    foreach (SearchResult rs in coll)
                    {
                        ResultPropertyCollection resultPropColl = rs.Properties;

                        foreach (Object memberColl in resultPropColl["member"])
                        {
                            DirectoryEntry gpMemberEntry = new DirectoryEntry("LDAP://" + memberColl);

                            System.DirectoryServices.PropertyCollection userProps = gpMemberEntry.Properties;


                            //getting user properties from AD

                            object obVal = userProps["displayName"].Value;
                            object obAcc = userProps["sAMAccountName"].Value;


                            //if (null != obVal)
                            //{
                            //    //groupMemebers.Add(obVal.ToString().ToLower());//display the display name
                            //    groupMemebers.Add(obAcc.ToString().ToLower());//Display the login
                            //}
                            //else groupMemebers.AddRange(GetGroupMembers(userProps["sAMAccountName"].Value.ToString()));

                        }
                    }
                }

                catch (Exception ex)
                {
                }

                // return groupMemebers;

            }
                //how to use use AD method
                 public string GetUserFullNameFromAD(string EmpNum)
                {
                    ADMethods adObj = new ADMethods();
                    ADMethods.ADAttributes obj = adObj.GetEmployeeAttributes(EmpNum, ADMethods.AdPrpoertyParameters.employeeid);
                    return obj.ManagerEmployeeID;         
                }

        }

    
}
