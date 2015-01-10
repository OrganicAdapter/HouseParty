using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.Core.Common.Models;
using Orchard.Core.Containers.Models;
using Orchard.Core.Contents.Settings;
using Orchard.Core.Contents.ViewModels;
using Orchard.Data;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Logging;
using Orchard.Mvc.Extensions;
using Orchard.Mvc.Html;
using Orchard.UI.Navigation;
using Orchard.UI.Notify;
using Orchard.Settings;
using Orchard.Utility.Extensions;
using Orchard.Themes;
using Orchard;
using Orchard.Core.Contents;
using SoftIT.HouseParty.Constants;

namespace SoftIT.HouseParty.Controllers
{
    [Themed]
    [Authorize]
    public class PartyController : Controller, IUpdateModel
    {
        private readonly IContentManager _contentManager;
        private readonly IOrchardServices _orchardServices;
        private readonly ITransactionManager _transactionManager;

        public Localizer T { get; set; }

        public PartyController(IContentManager contentManager, IOrchardServices orchardServices, ITransactionManager transactionManager)
        {
            _contentManager = contentManager;
            _orchardServices = orchardServices;
            _transactionManager = transactionManager;
            T = NullLocalizer.Instance;
        }

        public ActionResult Create()
        {
            var party = _contentManager.New(ContentTypes.Party);

            var viewModel = _orchardServices.New.ViewModel(
                PartyEditor: _contentManager.BuildEditor(party));

            return View(viewModel);
        }

        [HttpPost, ActionName("Create")]
        [Orchard.Mvc.FormValueRequired("submit.Save")]
        public ActionResult CreatePOST(string id, string returnUrl)
        {
            return CreatePOST(id, returnUrl, contentItem =>
            {
                if (!contentItem.Has<IPublishingControlAspect>() && !contentItem.TypeDefinition.Settings.GetModel<ContentTypeSettings>().Draftable)
                    _contentManager.Publish(contentItem);
            });
        }

        private ActionResult CreatePOST(string id, string returnUrl, Action<ContentItem> conditionallyPublish)
        {
            var contentItem = _contentManager.New(id);

            if (!_orchardServices.Authorizer.Authorize(Permissions.EditContent, contentItem, T("Couldn't create content")))
                return new HttpUnauthorizedResult();

            _contentManager.Create(contentItem, VersionOptions.Draft);

            var model = _contentManager.UpdateEditor(contentItem, this);

            if (!ModelState.IsValid)
            {
                _transactionManager.Cancel();
                return View(model);
            }

            conditionallyPublish(contentItem);

            _orchardServices.Notifier.Information(string.IsNullOrWhiteSpace(contentItem.TypeDefinition.DisplayName)
                ? T("Your content has been created.")
                : T("Your {0} has been created.", contentItem.TypeDefinition.DisplayName));
            if (!string.IsNullOrEmpty(returnUrl))
            {
                return this.RedirectLocal(returnUrl);
            }
            var adminRouteValues = _contentManager.GetItemMetadata(contentItem).AdminRouteValues;
            return RedirectToRoute(adminRouteValues);
        }

        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}