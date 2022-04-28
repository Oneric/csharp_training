using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace WebAddressbookTests
{
    public class GroupHelper : HelperBase
    {
        public GroupHelper(ApplicationManager manager) : base(manager)
        {
        }
        public GroupHelper Create(GroupData group)
        {
            manager.Navigation.GoToGroupsPage();

            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();

            return this;
        }

        public GroupHelper Remove(int v)
        {

            manager.Navigation.GoToGroupsPage();

            SelectGroup(v);
            RemoveSelectedGroups();

            return this;
        }

        public GroupHelper Modify(int v, GroupData modyfiedGroup)
        {
            manager.Navigation.GoToGroupsPage();

            SelectGroup(v);
            InitModifySelectedGroup();
            FillGroupForm(modyfiedGroup);
            SubmitGroupModify();
            ReturnToGroupsPage();

            return this;
        }

        public GroupHelper InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();

            return this;
        }
        public GroupHelper FillGroupForm(GroupData group)
        {
            FeelingTextInput(By.Name("group_name"), group.Name);
            FeelingTextInput(By.Name("group_header"), group.Header);
            FeelingTextInput(By.Name("group_footer"), group.Footer);
            return this;
        }

        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();

            return this;
        }
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();

            return this;
        }
        public GroupHelper SubmitGroupModify()
        {
            driver.FindElement(By.Name("update")).Click();

            return this;
        }

        public GroupHelper InitModifySelectedGroup()
        {
            driver.FindElement(By.Name("edit")).Click();

            return this;
        }

        public GroupHelper SelectGroup(int p)
        {
            driver.FindElement(By.XPath("//span[@class=\"group\"][" + p + "]/input")).Click();

            return this;
        }
        public GroupHelper RemoveSelectedGroups()
        {
            driver.FindElement(By.Name("delete")).Click();

            return this;
        }
        public bool IsExistsGroup(int v)
        {
            return IsElementPresent(By.XPath("//span[@class=\"group\"][" + v + "]/input"));
        }
    }
}
