SetCompressor /SOLID /FINAL lzma

;-------------------------------------------------------------------------------
;Some defines
;-------------------------------------------------------------------------------

!define APPNAME "Visual NHibernate"

; Where are the files to be installed located.
!define PATCH_SOURCE_ROOT "C:\PathOfNewFiles"

; Where are the "*.pat" files.
!define PATCH_FILES_ROOT "C:\Users\Gareth\Desktop\VPatch test"

;Request application privileges for Windows Vista/7
RequestExecutionLevel admin

; The default installation directory
InstallDir "$PROGRAMFILES\${APPNAME}"

; The text to prompt the user to enter a directory
DirText "Select the Visual NHibernate install folder"

; Show details
ShowInstDetails hide

; Directory to which the files will be installed.
!define PATCH_INSTALL_ROOT $INSTDIR

;-------------------------------------------------------------------------------
; Installer fundamentals...
;------------------------------------------------------------------------------- 

; The name of the installer
Name "Visual NHibernate patch"
Caption ""
XPStyle on

BrandingText "www.slyce.com"

; The file to write
OutFile "testInstaller.exe"

;-------------------------------------------------------------------------------
;Stuff to install
;------------------------------------------------------------------------------- 

!include "NewPatch.nsi"

Section "Test Installer Core"

  SectionIn RO
  
  SetOutPath $INSTDIR
  
  Call patchFilesRemoved
  Call patchDirectoriesRemoved
  Call patchDirectoriesAdded
  Call patchFilesAdded
  Call patchFilesModified

SectionEnd


