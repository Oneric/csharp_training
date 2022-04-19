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

        public GroupHelper Modify(int p, GroupData modyfiedGroup)
        {
            manager.Navigation.GoToGroupsPage();

            SelectGroup(p);
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
            driver.FindElement(By.Name("group_name")).Click();
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            driver.FindElement(By.Name("group_header")).Click();
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            driver.FindElement(By.Name("group_footer")).Click();
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);

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
    }
}
