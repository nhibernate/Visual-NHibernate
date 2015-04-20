; Setup for ArchAngel v 2
SetCompressor /SOLID /FINAL lzma

;--------------------------------
;General

  !define CREATE_ARCHANGEL "true"
  !define CREATE_VISUAL_NHIBERNATE "true"

  ; Values that should be set by SlyceBuilder
  ;-------------------------------------------
  !define VERSION "2.0.13.1047"
  !define INSTALLERFOLDER "G:\Projects\Slyce\ArchAngel\trunk\Installer"
  !define NH_TEMPLATE_FOLDER "G:\Projects\Slyce\ArchAngel\trunk\ArchAngel.Templates"

  !ifdef CREATE_ARCHANGEL
	  !define APPNAME "Visual NHibernate"
	  !define APP_GUID "{402E043D-0FB0-495E-B18A-CC31959835C7}"
	  !define CONTROL_PANEL_TIP "ArchAngel"
	  !define EXE_TO_RUN_ON_FINISH "ArchAngel Workbench.exe"
  !else ifdef CREATE_VISUAL_NHIBERNATE
	  !define APPNAME "Visual NHibernate"
	  !define APP_GUID "{FB347C64-B9B6-4134-91AF-1E82B5AB8913}"
	  !define CONTROL_PANEL_TIP "Visual NHibernate"
	  !define EXE_TO_RUN_ON_FINISH "Visual NHibernate.exe"
  !endif

  !define APPNAMEANDVERSION "${APPNAME} ${VERSION}"
  !define STAGINGFOLDER "${INSTALLERFOLDER}\Staging"
  !define CONTROL_HELP_LINK "http://www.slyce.com"
  
  !include "MUI2.nsh"
  !include "registerExtension.nsh"
  !include "DotNetSearch.nsh"

  Name "${APPNAMEANDVERSION}"
  OutFile "${INSTALLERFOLDER}\NSIS\Output\${APPNAME} Setup ${VERSION}.exe"
  InstallDir "$PROGRAMFILES\${APPNAME}"
  InstallDirRegKey HKLM "Software\${APPNAME}" ""

  ;Request application privileges for Windows Vista
  RequestExecutionLevel admin

;--------------------------------
;Interface Settings

  !define MUI_ABORTWARNING
;  !define MUI_ABORTWARNING_TEXT "Cancel installation?"
  !define MUI_LICENSEPAGE_BUTTON "I Accept"
  !define MUI_LICENSEPAGE_TEXT_BOTTOM "If you accept the terms of the agreement, click 'I Accept' to continue. You must accept the agreement to install Visual NHibernate."
  !define MUI_FINISHPAGE_RUN "$INSTDIR\${EXE_TO_RUN_ON_FINISH}"
  !define MUI_FINISHPAGE_RUN_TEXT "Run ${APPNAME}"
  !define MUI_WELCOMEFINISHPAGE_BITMAP "${INSTALLERFOLDER}\NSIS\images\wizard.bmp"
  !define MUI_UNWELCOMEFINISHPAGE_BITMAP "${INSTALLERFOLDER}\NSIS\images\wizard_uninstall.bmp"
  !define MUI_ICON "${INSTALLERFOLDER}\NSIS\images\install.ico"
  !define MUI_UNICON "${INSTALLERFOLDER}\NSIS\images\uninstall.ico"
  !define MUI_HEADERIMAGE
  !define MUI_HEADERIMAGE_BITMAP "${INSTALLERFOLDER}\NSIS\images\header.bmp"
  !define MUI_HEADERIMAGE_UNBITMAP "${INSTALLERFOLDER}\NSIS\images\header.bmp"

;--------------------------------
;Pages

  !insertmacro MUI_PAGE_WELCOME
  !insertmacro MUI_PAGE_LICENSE "${INSTALLERFOLDER}\license.rtf"
  !insertmacro MUI_PAGE_DIRECTORY
  !insertmacro MUI_PAGE_INSTFILES
  !insertmacro MUI_PAGE_FINISH

  !insertmacro MUI_UNPAGE_WELCOME
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
  !insertmacro MUI_UNPAGE_FINISH

;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;File Info

VIProductVersion "${VERSION}"
VIAddVersionKey /LANG=${LANG_ENGLISH} "ProductName" "${APPNAME}"
VIAddVersionKey /LANG=${LANG_ENGLISH} "Comments" "Installer for Visual NHibernate"
VIAddVersionKey /LANG=${LANG_ENGLISH} "CompanyName" "Slyce Software Limited"
VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalTrademarks" "${APPNAME} is a trademark of Slyce Software Limited"
VIAddVersionKey /LANG=${LANG_ENGLISH} "LegalCopyright" "Copyright 2010 Slyce Software Limited"
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileDescription" "${APPNAME} Installer"
VIAddVersionKey /LANG=${LANG_ENGLISH} "FileVersion" "${VERSION}"
;--------------------------------
;Macros

; Add program to Control Panel. See: http://nsis.sourceforge.net/Adding_your_program_to_the_Control_Panel
!define WControlPanelItem_Add "!insertmacro WControlPanelItem_Add"
!macro WControlPanelItem_Add GUID NAME TIP EXEC ICON

  WriteRegStr HKCR "CLSID\${GUID}" "" "${NAME}"
  WriteRegStr HKCR "CLSID\${GUID}" "InfoTip" "${TIP}"
  WriteRegStr HKCR "CLSID\${GUID}\DefaultIcon" "" "${ICON}"
  WriteRegStr HKCR "CLSID\${GUID}\Shell\Open\Command" "" "${EXEC}"
  WriteRegDWORD HKCR "CLSID\${GUID}\ShellFolder" "Attributes" "0"
  ;WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ControlPanel\NameSpace\${GUID}" "" "${NAME}"

  WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "" "${NAME}"
  WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "DisplayName" "${NAME}"
  WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "DisplayIcon" "$INSTDIR\${EXE_TO_RUN_ON_FINISH},0"
  WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "DisplayVersion" "${VERSION}"
  WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "FriendlyName" "${NAME}"
  WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "HelpLink" "${CONTROL_HELP_LINK}"
  WriteRegExpandStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "InstallLocation" "$INSTDIR"
  WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "NoModify" "1"
  WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "NoRepair" "1"
  WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "Publisher" "Slyce Software"
  WriteRegExpandStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "UninstallString" "$INSTDIR\uninstall.exe"
  WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}" "URLInfoAbout" "${CONTROL_HELP_LINK}"

!macroend
 
!define WControlPanelItem_Remove "!insertmacro WControlPanelItem_Remove"
!macro WControlPanelItem_Remove "GUID"
  DeleteRegKey HKCR "CLSID\${GUID}"
  ;DeleteRegKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ControlPanel\NameSpace\${GUID}"
  DeleteRegKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\${GUID}"
!macroend

;--------------------------------

Section "ArchAngel" Section1

	; Set Section properties
	SetOverwrite on

	; Set Section Files and Shortcuts
	SetOutPath "$INSTDIR\"
	File "${STAGINGFOLDER}\ActiproSoftware.Shared.Net20.dll"
	File "${STAGINGFOLDER}\ActiproSoftware.SyntaxEditor.Addons.DotNet.Net20.dll"
	File "${STAGINGFOLDER}\ActiproSoftware.SyntaxEditor.Net20.dll"
	File "${STAGINGFOLDER}\ActiproSoftware.UIStudio.Dock.Net20.dll"
	File "${STAGINGFOLDER}\ActiproSoftware.WinUICore.Net20.dll"
	File "${STAGINGFOLDER}\Antlr3.Runtime.dll"
	File "${STAGINGFOLDER}\ArchAngel.Common.dll"
	File "${STAGINGFOLDER}\ArchAngel.NHibernateHelper.dll"
	File "${STAGINGFOLDER}\ArchAngel.Interfaces.dll"
	File "${STAGINGFOLDER}\ArchAngel.Providers.CodeProvider.dll"
	File "${STAGINGFOLDER}\ArchAngel.Providers.EntityModel.dll"
	File "${STAGINGFOLDER}\ArchAngel.Workbench.exe.config"
	File "${STAGINGFOLDER}\Devart.Data.Universal.dll"
	File "${STAGINGFOLDER}\Devart.Data.Universal.MySql.dll"
	File "${STAGINGFOLDER}\Devart.Data.Universal.Oracle.dll"
	File "${STAGINGFOLDER}\Devart.Data.Universal.PostgreSql.dll"
	File "${STAGINGFOLDER}\Devart.Data.dll"
	File "${STAGINGFOLDER}\FirebirdSql.Data.FirebirdClient.dll"
	File "${STAGINGFOLDER}\DevComponents.DotNetBar2.dll"
	File "${STAGINGFOLDER}\Inflector.Net.dll"
	;File "${STAGINGFOLDER}\QuickGraph.dll"
	;File "${STAGINGFOLDER}\QuickGraph.Graphviz.dll"
	;File "${STAGINGFOLDER}\GraphSharp.dll"
	;File "${STAGINGFOLDER}\SchemaDiagrammer.dll"
	File "${STAGINGFOLDER}\Slyce.Common.dll"
	File "${STAGINGFOLDER}\Slyce.IntelliMerge.dll"
	File "${STAGINGFOLDER}\log4net.dll"
	File "${STAGINGFOLDER}\NHibernate.AAT.DLL"
	File "${STAGINGFOLDER}\Vista Api.DLL"
	File "${STAGINGFOLDER}\sqlite3.exe"
	File "images\Workbench.ico"
	File "images\website.ico"
	File "${INSTALLERFOLDER}\license.pdf"
	File "${STAGINGFOLDER}\ArchAngel.Common.pdb"
	File "${STAGINGFOLDER}\ArchAngel.NHibernateHelper.pdb"
	File "${STAGINGFOLDER}\ArchAngel.Interfaces.pdb"
	File "${STAGINGFOLDER}\ArchAngel.Providers.CodeProvider.pdb"
	File "${STAGINGFOLDER}\ArchAngel.Providers.EntityModel.pdb"
	;File "${STAGINGFOLDER}\SchemaDiagrammer.pdb"
	File "${STAGINGFOLDER}\Slyce.Common.pdb"
	File "${STAGINGFOLDER}\Slyce.IntelliMerge.pdb"
	File "${STAGINGFOLDER}\NHibernate.AAT.pdb"
	File "${STAGINGFOLDER}\ArchAngel.Workbench.pdb"
	File "${STAGINGFOLDER}\Visual NHibernate.exe"
	File /oname=FluentNhibernate.dll "${NH_TEMPLATE_FOLDER}\NHibernate_files\Resources\NH2 FluentNhibernate.dll"
	File /oname=Nhibernate.dll "${NH_TEMPLATE_FOLDER}\NHibernate_files\Resources\NH2 Nhibernate.dll"
	File /oname=Iesi.Collections.dll "${NH_TEMPLATE_FOLDER}\NHibernate_files\Resources\NH2 Iesi.Collections.dll"
	;File /oname=Antlr3.Runtime.dll "${NH_TEMPLATE_FOLDER}\NHibernate_files\Resources\NH2 Antlr3.Runtime.dll"

	; Create Templates
	; NHibernate template
	CreateDirectory "$INSTDIR\Templates"
	SetOutPath "$INSTDIR\Templates"
	File "${NH_TEMPLATE_FOLDER}\NHibernate.vnh_template"

	CreateDirectory "$INSTDIR\Templates\NHibernate_files"
	SetOutPath "$INSTDIR\Templates\NHibernate_files"
	File "${NH_TEMPLATE_FOLDER}\NHibernate_files\*.*"

	CreateDirectory "$INSTDIR\Templates\NHibernate_files\Resources"
	SetOutPath "$INSTDIR\Templates\NHibernate_files\Resources"
	File "${NH_TEMPLATE_FOLDER}\NHibernate_files\Resources\*.*"

	; SHarp Architecture template
	SetOutPath "$INSTDIR\Templates"
	File "${NH_TEMPLATE_FOLDER}\SharpArchitecture.vnh_template"

	CreateDirectory "$INSTDIR\Templates\SharpArchitecture_files"
	SetOutPath "$INSTDIR\Templates\SharpArchitecture_files"
	File "${NH_TEMPLATE_FOLDER}\SharpArchitecture_files\*.*"

	CreateDirectory "$INSTDIR\Templates\SharpArchitecture_files\Resources"
	SetOutPath "$INSTDIR\Templates\SharpArchitecture_files\Resources"
	File "${NH_TEMPLATE_FOLDER}\SharpArchitecture_files\Resources\*.*"

	; Create Templates folder is user's 'My Documents' folder
	CreateDirectory "$DOCUMENTS\Visual NHibernate\Templates"
	CreateDirectory "$DOCUMENTS\Visual NHibernate\Projects"

	;; Create Graphviz folder and decompress 7zip file into it
	;CreateDirectory "$INSTDIR\Graphviz"
	;SetOutPath "$INSTDIR\Graphviz"
	;File "${INSTALLERFOLDER}\NSIS\Graphviz"
	;Nsis7z::ExtractWithDetails "Graphviz" "Installing libraries %s..."
	;Delete "$INSTDIR\Graphviz\Graphviz"

	; Create Shortcuts
	CreateDirectory "$SMPROGRAMS\${APPNAME}"

	!ifdef CREATE_ARCHANGEL
		CreateShortCut "$SMPROGRAMS\${APPNAME}\ArchAngel Workbench.lnk" "$INSTDIR\ArchAngel.Workbench.exe" "" "$INSTDIR\ArchAngel.Workbench.exe" "" "" "" "Create and edit ArchAngel projects"
		CreateShortCut "$SMPROGRAMS\${APPNAME}\ArchAngel Designer.lnk" "$INSTDIR\ArchAngel.Designer.exe" "" "$INSTDIR\ArchAngel.Designer.exe" "" "" "" "Create and edit ArchAngel templates"
	!else ifdef CREATE_VISUAL_NHIBERNATE
		CreateShortCut "$SMPROGRAMS\${APPNAME}\Visual NHibernate.lnk" "$INSTDIR\Visual NHibernate.exe" "" "$INSTDIR\Visual NHibernate.exe"  "" "" "" "Create and edit NHibernate projects"
	!endif

	;Peg menu order
	nsisStartMenu::RegenerateFolder "${APPNAME}"
	CreateShortCut "$SMPROGRAMS\${APPNAME}\Website.lnk" "http://www.slyce.com" "" "$INSTDIR\website.ico" "0" "" "" "Go to the Slyce Software website - home of ${APPNAME}"
	;Peg menu order
	nsisStartMenu::RegenerateFolder "${APPNAME}"
	CreateShortCut "$SMPROGRAMS\${APPNAME}\Uninstall.lnk" "$INSTDIR\uninstall.exe" "" "" "" "" "" "Uninstall ${APPNAME}"

SectionEnd

Section -FinishSection

	WriteRegStr HKLM "Software\${APPNAME}" "" "$INSTDIR"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "DisplayName" "${APPNAME} ${VERSION}"
	WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" "UninstallString" "$INSTDIR\uninstall.exe"
	WriteUninstaller "$INSTDIR\uninstall.exe"

	; Register file extensions

	!ifdef CREATE_ARCHANGEL
		${registerExtension} "$INSTDIR\ArchAngel.Workbench.exe" ".aaproj" "ArchAngel Workbench File"
		${registerExtension} "$INSTDIR\ArchAngel.Designer.exe" ".stz" "ArchAngel Designer File"
	!else ifdef CREATE_VISUAL_NHIBERNATE
		${registerExtension} "$INSTDIR\Visual NHibernate.exe" ".aaproj" "Visual NHibernate File"
		${registerExtension} "$INSTDIR\Visual NHibernate.exe" ".SlyceLicense" "Slyce License file"
	!endif

	; Add to Control Panel
	${WControlPanelItem_Add} "${APP_GUID}" "${APPNAME}" "${CONTROL_PANEL_TIP}" "$INSTDIR\uninstall.exe" "$INSTDIR\Workbench.ico"

SectionEnd

;Uninstall section
Section Uninstall

	;Remove from registry...
	DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}"
	DeleteRegKey HKLM "SOFTWARE\${APPNAME}"

	; Delete self
	Delete "$INSTDIR\uninstall.exe"

	; Delete Shortcuts
	Delete "$SMPROGRAMS\${APPNAME}\Uninstall.lnk"
	Delete "$SMPROGRAMS\${APPNAME}\Website.lnk"

	!ifdef CREATE_ARCHANGEL
		Delete "$SMPROGRAMS\${APPNAME}\ArchAngel Workbench.lnk"
		Delete "$SMPROGRAMS\${APPNAME}\ArchAngel Designer.lnk"	
	!else ifdef CREATE_VISUAL_NHIBERNATE
		Delete "$SMPROGRAMS\${APPNAME}\Visual NHibernate.lnk"
	!endif

	; Clean up ArchAngel
	Delete "$INSTDIR\ActiproSoftware.SyntaxEditor.Addons.DotNet.Net20.dll"
	Delete "$INSTDIR\ActiproSoftware.Shared.Net20.dll"
	Delete "$INSTDIR\ActiproSoftware.SyntaxEditor.Net20.dll"
	Delete "$INSTDIR\ActiproSoftware.UIStudio.Dock.Net20.dll"
	Delete "$INSTDIR\ActiproSoftware.WinUICore.Net20.dll"
	Delete "$INSTDIR\ArchAngel.Common.dll"
	Delete "$INSTDIR\ArchAngel.NHibernateHelper.dll"
	Delete "$INSTDIR\ArchAngel.Interfaces.dll"
	Delete "$INSTDIR\ArchAngel.Providers.CodeProvider.dll"
	Delete "$INSTDIR\ArchAngel.Providers.EntityModel.dll"
	Delete "$INSTDIR\ArchAngel.Workbench.exe.config"
	Delete "$INSTDIR\Devart.Data.Universal.dll"
	Delete "$INSTDIR\Devart.Data.Universal.MySql.dll"
	Delete "$INSTDIR\Devart.Data.Universal.Oracle.dll"
	Delete "$INSTDIR\Devart.Data.Universal.PostgreSql.dll"
	Delete "$INSTDIR\Devart.Data.dll"
	Delete "$INSTDIR\FirebirdSql.Data.FirebirdClient.dll"
	Delete "$INSTDIR\DevComponents.DotNetBar2.dll"
	Delete "$INSTDIR\Inflector.Net.dll"
	Delete "$INSTDIR\log-file.txt"
	;Delete "$INSTDIR\QuickGraph.dll"
	;Delete "$INSTDIR\QuickGraph.Graphviz.dll"
	;Delete "$INSTDIR\GraphSharp.dll"
	;Delete "$INSTDIR\SchemaDiagrammer.dll"
	Delete "$INSTDIR\Slyce.Common.dll"
	Delete "$INSTDIR\Slyce.IntelliMerge.dll"
	Delete "$INSTDIR\log4net.dll"
	Delete "$INSTDIR\Workbench.ico"
	Delete "$INSTDIR\website.ico"
	Delete "$INSTDIR\license.pdf"
	Delete "$INSTDIR\debugger.log"
	Delete "$INSTDIR\NHibernate.AAT.DLL"
	Delete "$INSTDIR\Vista Api.DLL"
	Delete "$INSTDIR\ArchAngel.Common.pdb"
	Delete "$INSTDIR\ArchAngel.NHibernateHelper.pdb"
	Delete "$INSTDIR\ArchAngel.Interfaces.pdb"
	Delete "$INSTDIR\ArchAngel.Providers.CodeProvider.pdb"
	Delete "$INSTDIR\ArchAngel.Providers.EntityModel.pdb"
	;Delete "$INSTDIR\SchemaDiagrammer.pdb"
	Delete "$INSTDIR\Slyce.Common.pdb"
	Delete "$INSTDIR\Slyce.IntelliMerge.pdb"
	Delete "$INSTDIR\NHibernate.AAT.pdb"
	Delete "$INSTDIR\ArchAngel.Workbench.pdb"
	Delete "$INSTDIR\Visual NHibernate.exe"
	Delete "$INSTDIR\FluentNhibernate.dll"
	Delete "$INSTDIR\Nhibernate.dll"
	Delete "$INSTDIR\Iesi.Collections.dll"
	Delete "$INSTDIR\Antlr3.Runtime.dll"
	Delete "$INSTDIR\sqlite3.exe"

	; Remove remaining directories
	RMDir /r $INSTDIR\Templates
	;RMDir /r $INSTDIR\Graphviz
	RMDir "$SMPROGRAMS\${APPNAME}"
	RMDir "$INSTDIR\"

	; Unregister file extensions
	!ifdef CREATE_ARCHANGEL
		${unregisterExtension} ".aaproj" "ArchAngel Workbench File"
		${unregisterExtension} ".stz" "ArchAngel Designer File"
	!else ifdef CREATE_VISUAL_NHIBERNATE
		${unregisterExtension} ".aaproj" "Visual NHibernate File"
	!endif

	; Remove from Control Panel
	${WControlPanelItem_Remove} "${APP_GUID}"

SectionEnd

Function .onInit

  Call CheckForRunningInstanceOfInstaller

  ;Check whether the app is running
  loopIsRunningCheck:
	FindProcDLL::FindProc "${APPNAME}.exe"
	IntCmp $R0 1 0 notRunning
	MessageBox MB_RETRYCANCEL|MB_ICONEXCLAMATION "${APPNAME} is running.$\n$\nClose it and press Retry, or press Cancel to exit." IDRETRY loopIsRunningCheck IDCANCEL abortnow

	abortnow:
		Abort
  notRunning:

  ;Check for required version of the .net framework
  !insertmacro DotNetSearch 4 0 30319 WARNING "http://www.microsoft.com/downloads/details.aspx?FamilyID=9cfb2d51-5ff4-4491-b0e5-b386f32c0992&displaylang=en"
  
  ; Auto-uninstall old version before installing new
  ReadRegStr $R0 HKLM \
  "Software\Microsoft\Windows\CurrentVersion\Uninstall\${APPNAME}" \
  "UninstallString"
  StrCmp $R0 "" done
 
  ;MessageBox MB_OKCANCEL|MB_ICONQUESTION \
  ;"${APPNAME} is already installed. $\n$\nClick `OK` to remove the \
  ;previous version or `Cancel` to cancel this upgrade." \
  ;IDOK uninst
  ;Abort
 
  ;Run the uninstaller
  ;uninst:
    ClearErrors
      Exec "$INSTDIR\uninstall.exe /S" ;silent uninstall
  done:
 
FunctionEnd

Function CheckForRunningInstanceOfInstaller
  
  System::Call 'kernel32::CreateMutexA(i 0, i 0, t "VNHSetup blah blah") ?e'
  Pop $R0
  StrCmp $R0 0 +3
    MessageBox MB_OK|MB_ICONEXCLAMATION "Another instance ${APPNAME} Setup is already running."
    Abort

FunctionEnd

BrandingText "www.slyce.com"

; eof