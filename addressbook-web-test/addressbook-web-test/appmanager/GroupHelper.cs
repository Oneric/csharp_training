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
        /// <summary>
        /// Набор шагов для создания новой группы
        /// </summary>
        /// <param name="group">Объект класса GroupData с данными для создания новой группы</param>
        /// <returns></returns>
        public GroupHelper Create(GroupData group)
        {
            InitNewGroupCreation();
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnToGroupsPage();

            return this;
        }
        /// <summary>
        /// Набор шагов для изменения группы с индексом v
        /// </summary>
        /// <param name="v">Индекс</param>
        /// <param name="modyfiedGroup">Объект класса GroupData с данными для изменения группы</param>
        /// <returns></returns>
        public GroupHelper Modify(int v, GroupData modyfiedGroup)
        {
            SelectGroup(v);
            InitModifySelectedGroup();
            FillGroupForm(modyfiedGroup);
            SubmitGroupModify();
            ReturnToGroupsPage();

            return this;
        }
        /// <summary>
        /// Набор шагов для изменения группы group.Id
        /// </summary>
        /// <param name="group">Объект класса GroupData</param>
        /// <param name="modyfiedGroup">Объект класса GroupData c данными для изменения группы</param>
        /// <returns></returns>
        public GroupHelper Modify(GroupData group, GroupData modyfiedGroup)
        {
            SelectGroup(group.Id);
            InitModifySelectedGroup();
            FillGroupForm(modyfiedGroup);
            SubmitGroupModify();
            ReturnToGroupsPage();

            return this;
        }
        /// <summary>
        /// Набор шагов для удаления группы с индексом v
        /// </summary>
        /// <param name="v">Индекс</param>
        /// <returns></returns>
        public GroupHelper Remove(int v)
        {
            SelectGroup(v);
            RemoveSelectedGroups();
            ReturnToGroupsPage();

            return this;
        }
        /// <summary>
        /// Набор шагов для удаления группы group.Id
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public GroupHelper Remove(GroupData group)
        {
            SelectGroup(group.Id);
            RemoveSelectedGroups();
            ReturnToGroupsPage();

            return this;
        }
        /// <summary>
        /// Нажимаем кнопку New group на странице groups
        /// </summary>
        /// <returns></returns>
        public GroupHelper InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();

            return this;
        }
        /// <summary>
        /// Заполняем форму создания группы
        /// </summary>
        /// <param name="group">Объект класса GroupData</param>
        /// <returns></returns>
        public GroupHelper FillGroupForm(GroupData group)
        {
            FeelingTextInput(By.Name("group_name"), group.Name);
            FeelingTextInput(By.Name("group_header"), group.Header);
            FeelingTextInput(By.Name("group_footer"), group.Footer);
            return this;
        }
        /// <summary>
        /// Нажимаем кнопку Enter information на форме создания группы
        /// </summary>
        /// <returns></returns>
        public GroupHelper SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            groupListCache = null;

            return this;
        }
        /// <summary>
        /// Возврат на страницу group page
        /// </summary>
        /// <returns></returns>
        public GroupHelper ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();

            return this;
        }
        /// <summary>
        /// Сохраняем изменения группы
        /// </summary>
        /// <returns></returns>
        public GroupHelper SubmitGroupModify()
        {
            driver.FindElement(By.Name("update")).Click();
            groupListCache = null;

            return this;
        }
        /// <summary>
        /// Нажимаем кнопку Edit
        /// </summary>
        /// <returns></returns>
        public GroupHelper InitModifySelectedGroup()
        {
            driver.FindElement(By.Name("edit")).Click();

            return this;
        }
        /// <summary>
        /// Активируем чекбокс группы с индексом v
        /// </summary>
        /// <param name="v">Индекс</param>
        /// <returns></returns>
        public GroupHelper SelectGroup(int v)
        {
            driver.FindElement(By.XPath("//span[@class=\"group\"][" + (v + 1) + "]/input")).Click();

            return this;
        }
        /// <summary>
        /// Активируем чекбокс группы с индексом Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GroupHelper SelectGroup(String id)
        {
            driver.FindElement(By.XPath("//span[@class=\"group\"]/input[@value=\"" + id + "\"]")).Click();

            return this;
        }
        /// <summary>
        /// Удаляем выбранную группу
        /// </summary>
        /// <returns></returns>
        public GroupHelper RemoveSelectedGroups()
        {
            driver.FindElement(By.Name("delete")).Click();
            groupListCache = null;

            return this;
        }
        /// <summary>
        /// Проверяем наличие группы с индексом v
        /// </summary>
        /// <param name="v">Индекс</param>
        /// <returns></returns>
        public bool IsExistsGroup(int v)
        {
            return IsElementPresent(By.XPath("//span[@class=\"group\"][" + (v + 1) + "]/input"));
        }

        private List<GroupData> groupListCache = null; 
        /// <summary>
        /// Получаем список групп
        /// </summary>
        /// <returns></returns>
        public List<GroupData> GetGroupList()
        {
            
            if (groupListCache == null)
            {
                groupListCache = new List<GroupData>();
                ICollection<IWebElement> elements = driver.FindElements(By.XPath("//span[@class=\"group\"]"));
                foreach (IWebElement element in elements)
                {
                    groupListCache.Add(
                        new GroupData(null)
                        {
                            Id = element.FindElement(By.XPath("./input")).GetAttribute("value"),
                        }
                    );
                }
                string allGroupNames = driver.FindElement(By.CssSelector("div#content form")).Text;
                string[] groupNames = allGroupNames.Split('\n');
                int shift = groupListCache.Count - groupNames.Length;
                for (int i = 0; i < groupListCache.Count; i++)
                {
                    if(i < shift)
                    {
                        groupListCache[i].Name = "";
                    }
                    else
                    {
                        groupListCache[i].Name = groupNames[i-shift].Trim();
                    }
                }
            }
            return new List<GroupData>(groupListCache);
        }
        /// <summary>
        /// Получаем количество групп
        /// </summary>
        /// <returns></returns>
        public int GetGroupCount()
        {
            return driver.FindElements(By.XPath("//span[@class=\"group\"]")).Count;
        }
    }
}
