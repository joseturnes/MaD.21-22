﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMaD.Web.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.10.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("6")]
        public int PracticaMaD_defaultCount {
            get {
                return ((int)(this["PracticaMaD_defaultCount"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Es.Udc.DotNet.PracticaMaD.Model.UserService.IUserService")]
        public string ObjectDS_User_Service {
            get {
                return ((string)(this["ObjectDS_User_Service"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ListOfFollows")]
        public string ObjectDS_User_Follows_SelectMethod {
            get {
                return ((string)(this["ObjectDS_User_Follows_SelectMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("startIndex")]
        public string ObjectDS_User_StartIndexParameter {
            get {
                return ((string)(this["ObjectDS_User_StartIndexParameter"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("count")]
        public string ObjectDS_User_CountParameter {
            get {
                return ((string)(this["ObjectDS_User_CountParameter"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("getNumberOfFollows")]
        public string ObjectDS_Follows_CountMethod {
            get {
                return ((string)(this["ObjectDS_Follows_CountMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FollowerList")]
        public string ObjectDS_User_Followers_SelectMethod {
            get {
                return ((string)(this["ObjectDS_User_Followers_SelectMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("getNumberOfFollowers")]
        public string ObjectDS_Followers_CountMethod {
            get {
                return ((string)(this["ObjectDS_Followers_CountMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Es.Udc.DotNet.PracticaMaD.Model.ImageUploadService.IImageUploadService")]
        public string ObjectDS_Image_Service {
            get {
                return ((string)(this["ObjectDS_Image_Service"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("recentUploads")]
        public string ObjectDS_Image_SelectMethod {
            get {
                return ((string)(this["ObjectDS_Image_SelectMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("getNumberOfImages")]
        public string ObjectDS_Images_CountMethod {
            get {
                return ((string)(this["ObjectDS_Images_CountMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("searchComments")]
        public string ObjectDS_Comments_SelectMethod {
            get {
                return ((string)(this["ObjectDS_Comments_SelectMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Es.Udc.DotNet.PracticaMaD.Model.CommentService.ICommentService")]
        public string ObjectDS_Comments_Service {
            get {
                return ((string)(this["ObjectDS_Comments_Service"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("CountComments")]
        public string ObjectDS_Comments_CountMethod {
            get {
                return ((string)(this["ObjectDS_Comments_CountMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("findMostUsedTags")]
        public string ObjectDS_Tag_SelectMethod {
            get {
                return ((string)(this["ObjectDS_Tag_SelectMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Es.Udc.DotNet.PracticaMaD.Model.TagService.ITagService")]
        public string ObjectDS_Tag_Service {
            get {
                return ((string)(this["ObjectDS_Tag_Service"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("countTags")]
        public string ObjectDS_Tag_CountMethod {
            get {
                return ((string)(this["ObjectDS_Tag_CountMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("fingImagesByTagId")]
        public string ObjectDS_Tag_Image_SelectMethod {
            get {
                return ((string)(this["ObjectDS_Tag_Image_SelectMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("countImagesWithTag")]
        public string ObjectDS_Tag_Images_CountMethod {
            get {
                return ((string)(this["ObjectDS_Tag_Images_CountMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FindByKeywordAndCategory")]
        public string ObjectDS_Search_keywords_SelectMethod {
            get {
                return ((string)(this["ObjectDS_Search_keywords_SelectMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("countSearchKeywords")]
        public string ObjectDS_Search_Keywords_CountMethod {
            get {
                return ((string)(this["ObjectDS_Search_Keywords_CountMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FindByKeyword")]
        public string ObjectDS_Search_keywords_without_category_SelectMethod {
            get {
                return ((string)(this["ObjectDS_Search_keywords_without_category_SelectMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("findRecentUploads")]
        public string ObjectDS_Recent_Image_SelectMethod {
            get {
                return ((string)(this["ObjectDS_Recent_Image_SelectMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("countRecentUploads")]
        public string ObjectDS_Recent_Images_CountMethod {
            get {
                return ((string)(this["ObjectDS_Recent_Images_CountMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("FindImageTags")]
        public string ObjectDS_Image_Tag_SelectMethod {
            get {
                return ((string)(this["ObjectDS_Image_Tag_SelectMethod"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("CountImageTags")]
        public string ObjectDS_Image_Tag_CountMethod {
            get {
                return ((string)(this["ObjectDS_Image_Tag_CountMethod"]));
            }
        }
    }
}
