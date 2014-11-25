;//////////////////////////////////////////////////////////////////// OpenLink START (http://nsis.sourceforge.net/Open_link_in_new_browser_window)  /////////////////////////////////////////////////////////////
Function openLinkNewWindow
	Push $3
	Exch
	Push $2
	Exch
	Push $1
	Exch
	Push $0
	Exch

	ReadRegStr $0 HKCR "http\shell\open\command" ""
	# Get browser path
	DetailPrint $0
	StrCpy $2 '"'
	StrCpy $1 $0 1
	StrCmp $1 $2 +2 # if path is not enclosed in " look for space as final char
	StrCpy $2 ' '
	StrCpy $3 1
	loop:
	StrCpy $1 $0 1 $3
	DetailPrint $1
	StrCmp $1 $2 found
	StrCmp $1 "" found
	IntOp $3 $3 + 1
	Goto loop

	found:
	StrCpy $1 $0 $3
	StrCmp $2 " " +2
	StrCpy $1 '$1"'

	Pop $0
	Exec '$1 $0'
	Pop $0
	Pop $1
	Pop $2
	Pop $3
FunctionEnd
 
!macro _OpenURL URL
	Push "${URL}"
	Call openLinkNewWindow
!macroend
 
!define OpenURL '!insertmacro "_OpenURL"'
;//////////////////////////////////////////////////////////////////// OpenLink END /////////////////////////////////////////////////////////////

!macro IfKeyExists ROOT MAIN_KEY KEY
	push $R0
	push $R1
	 
	!define Index 'Line${__LINE__}'
	 
	StrCpy $R1 "0"
	 
	"${Index}-Loop:"
	; Check for Key
	EnumRegKey $R0 ${ROOT} "${MAIN_KEY}" "$R1"
	StrCmp $R0 "" "${Index}-False"
	  IntOp $R1 $R1 + 1
	  StrCmp $R0 "${KEY}" "${Index}-True" "${Index}-Loop"
	 
	"${Index}-True:"
	;Return 1 if found
	push "1"
	goto "${Index}-End"
	 
	"${Index}-False:"
	;Return 0 if not found
	push "0"
	goto "${Index}-End"
	 
	"${Index}-End:"
	!undef Index
	exch 2
	pop $R0
	pop $R1
!macroend

;//////////////////////////////////////////////////////////////////// DotNetSearch macro START (http://nsis.sourceforge.net/How_to_insure_a_required_version_of_.NETFramework_is_installed) /////////////////////////////
!macro DotNetSearch DOTNETVMAJOR DOTNETVMINOR DOTNETVMINORMINOR DOTNETLASTFUNCTION DOTNETPATH
	Var /GLOBAL DOTNET1
	Var /GLOBAL DOTNET2
	Var /GLOBAL DOTNET3
	Var /GLOBAL DOTNET4
	Var /GLOBAL DOTNET5
	Var /GLOBAL DOTNET6
	Var /GLOBAL gfhShortVersionString
		Push $DOTNET1
		Push $DOTNET2
		Push $DOTNET3
		Push $DOTNET4
		Push $DOTNET5
		Push $DOTNET6
 
			StrCpy $DOTNET1 "0"
			StrCpy $DOTNET2 "SOFTWARE\Microsoft\.NETFramework"
			StrCpy $DOTNET3 0
 
	DotNetStartEnum:
		EnumRegKey $DOTNET4 HKLM "$DOTNET2\policy" $DOTNET3
			StrCmp $DOTNET4 "" noDotNet dotNetFound
 
	dotNetFound:
		StrCpy $DOTNET5 $DOTNET4 1 0
		StrCmp $DOTNET5 "v" +1 goNextDotNet
		StrCpy $DOTNET5 $DOTNET4 1 1

	IntCmp $DOTNET5 ${DOTNETVMAJOR} +1 goNextDotNet yesDotNetReg
	StrCpy $DOTNET5 $DOTNET4 1 3
	IntCmp $DOTNET5 ${DOTNETVMINOR} +1 goNextDotNet yesDotNetReg
 


	StrCmp ${DOTNETVMINORMINOR} "" +1 yesDotNetReg
	IntCmpU $DOTNET5 ${DOTNETVMINORMINOR} yesDotNetReg goNextDotNet yesDotNetReg
	;MessageBox MB_YESNO|MB_ICONQUESTION "GFH dotnet4=$DOTNET4  dotnet6=$DOTNET6 dotnetminorminor= ${DOTNETVMINORMINOR}" IDYES +2 IDNO +1
	;IntCmp $DOTNET5 ${DOTNETVMINORMINOR} +1 goNextDotNet yesDotNetReg
 
		goNextDotNet:
			IntOp $DOTNET3 $DOTNET3 + 1
			Goto DotNetStartEnum
 
	yesDotNetReg: 
		EnumRegValue $DOTNET3 HKLM "$DOTNET2\policy\$DOTNET4" 0
		StrCmp $DOTNET3 "" noDotNet
		IntCmp $DOTNET3 ${DOTNETVMINORMINOR} +1 goNextDotNet +1
		; Check for Full version
		StrCpy $gfhShortVersionString $DOTNET4 2 0
		EnumRegValue $1 HKLM "Software\Microsoft\NET Framework Setup\NDP\$gfhShortVersionString\Full" $0
		IfErrors nClientNotFull isFullVersion
		isFullVersion:
		ReadRegStr $DOTNET5 HKLM $DOTNET2 "InstallRoot"
		StrCmp $DOTNET5 "" noDotNet
		StrCpy $DOTNET5 "$DOTNET5$DOTNET4.$DOTNET3\mscorlib.dll"
		IfFileExists $DOTNET5 yesDotNet noDotNet
 
 	nClientNotFull:
		StrCmp ${DOTNETLASTFUNCTION} "WARNING" +1 nDN4
			MessageBox MB_OK|MB_ICONEXCLAMATION \
			"You have the CLIENT version of the .NET Framework 4.0 installed. You need the FULL version.$\n$\nClick OK open the download page and install before continuing this setup." \
			IDOK 0
			StrCpy $DOTNET1 0
			${OpenURL} '${DOTNETPATH}'
			Goto DotNetFinish
	noDotNet:
		StrCmp ${DOTNETLASTFUNCTION} "INSTALL_ABORT" +1 nDN2
			MessageBox MB_YESNO|MB_ICONQUESTION \
			"You must have Microsoft .NET Framework version ${DOTNETVMAJOR}.${DOTNETVMINOR}.${DOTNETVMINORMINOR}$\nor higher installed. Install now?" \
			IDYES +2 IDNO +1
			Abort
			ExecWait '${DOTNETPATH}'
			Goto DotNetStartEnum
	nDN2:
		StrCmp ${DOTNETLASTFUNCTION} "INSTALL_NOABORT" +1 nDN3
			MessageBox MB_YESNO|MB_ICONQUESTION \
			"Microsoft .NET Framework version ${DOTNETVMAJOR}.${DOTNETVMINOR}.${DOTNETVMINORMINOR} is not installed.$\nDo so now?" \
			IDYES +1 IDNO +3
			ExecWait '${DOTNETPATH}'
			Goto DotNetStartEnum
			StrCpy $DOTNET1 0
			Goto DotNetFinish
	nDN3:
		StrCmp ${DOTNETLASTFUNCTION} "WARNING" +1 nDN4
			MessageBox MB_OK|MB_ICONEXCLAMATION \
			"Microsoft .NET Framework ${DOTNETVMAJOR}.${DOTNETVMINOR}.${DOTNETVMINORMINOR} is not installed.$\n$\nClick OK open the download page and install before continuing this setup." \
			IDOK 0
			StrCpy $DOTNET1 0
			${OpenURL} '${DOTNETPATH}'
			Goto DotNetFinish
	nDN4:
		StrCmp ${DOTNETLASTFUNCTION} "ABORT" +1 nDN5
			MessageBox MB_OK|MB_ICONEXCLAMATION \
			"Microsoft .NET Framework ${DOTNETVMAJOR}.${DOTNETVMINOR}.${DOTNETVMINORMINOR} is not installed.$\n$\nClick OK open the download page, install, then run this setup again." \
			IDOK 0
			${OpenURL} '${DOTNETPATH}'
			Abort
	nDN5:
		StrCmp ${DOTNETLASTFUNCTION} "IGNORE" +1 nDN6
			StrCpy $DOTNET1 0
			Goto DotNetFinish
	nDN6:
		MessageBox MB_OK \
		"$(^Name) Setup internal error.$\nMacro 'DotNetSearch', parameter '4'(${DOTNETLASTFUNCTION})invalid.$\nValue must be INSTALL_ABORT|INSTALL_NOABORT|WARNING|ABORT|IGNORE$\nSorry for the inconvenience.$\n$\tAborting..." \
		IDOK 0
		Abort
 
	yesDotNet:
		StrCpy $DOTNET1 1
 
	DotNetFinish:
		Pop $DOTNET6
		Pop $DOTNET5
		Pop $DOTNET4
		Pop $DOTNET3
		Pop $DOTNET2
		!define ${DOTNETOUTCOME} $DOTNET1
		Exch $DOTNET1
!macroend