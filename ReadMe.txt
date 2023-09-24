[v] Add EFModels
首頁html改成cshtml

=====
[V]add 註冊會員功能
	add Models/Infra/HashUtility.cs
	add AppSettings,<add key="salt" value="ar!Zu@D691RR"/>
	add Models/ViewModels/RegisterVm.cs
	add RegisterExts class,擴充方法 ToMember(RegisterVM)
	add Controllers/Members/MembersController
		add Register action(Get,Post)
		add Views/Memebrs/Register.cshml(**新增demo按鈕), RegisterConfirm.cshml 
	密碼設定要求(必須包含1個特殊符號且不得少於8碼)
	
[V]	發送confirm email註冊信件
	已完成google email寄送註冊功能
	!**修改Email Helper(SendViaGoogle改成寄信)
	!**modify MembersController
		edit RegisterMember // 發送驗證信
		add GenerateConfirmationLink

[V] 實作 新會員Email確認功能 
	會員啟用的url: /Members/ActiveRegister?memberId=99&confirmCode=ttttttttttt
	modify MembersController
		add ActiveRegister(memberId, confirmCode)
	add Views/Members/ActiveRegister.cshtml

[V] 密碼新增眼睛顯示

[V] 實作 登入登出網站
	只有帳密正確而且已開通會員才允許登入，實作之前請先個別建立已/未開通的會議紀錄
	
	目前已完成登入登出功能

	modify web.config, add Authenthcation node
	add Models/ViewModels/LoginVm.cs
	modify MemberController
		add Login action(Get, Post)
		add Logout action(get, only)
	modify _layout,加入login,logout link

	Controller 加入[Authorize] 登入才能用

[V] 實作 建立會員專區/編輯會員資料
	add Models/ViewModels/EditProfileVm.cs
	add Models/ViewModels/MemberExts class, 擴充方法 ToEditProfileVm(Member)

	modify MemberController
		add Index action 會員專區頁,在登入成功之後會導向此頁
			add Views/Members/Index.cshtml(空白範本)，選單模組Views/Shared/_mod_membernav.cshtml
		修改web.config裡的defaultUrl="/Members/Index/"
		add EditProfile action(Get,Post)
			抓取編輯資料
	add MemberExts
	已成功編輯會員資料!

[V]實作變更密碼
	add Models/ViewModels/EditPasswordVm.cs
	
	modifty MemberController
		add EditPassword action(Get, Post)
		add View/Members/EditPassword.cshtml(用create template)


[V]忘記密碼/重設密碼
	add Models/ViewModels/ForgotPasswordVm.cs
	modifty MemberController
		add ForgotPassword action(Get, Post)
		add ForgotPassword view Page(create template)
		add Views/Members/ForgotPasswordConfirm.cshtml  view Page(空白範例)

		修改 Views/Members/Login.cshtml 加入"忘記密碼"連結

[working on] 實作重設密碼
	add Models/ViewModels/ResetPasswordVm.cs 用來重新輸入密碼

	modifty MemberController
		add ResetPassword action(Get, Post)
		add Views/Members/ResetPassword.cshtml //重新設定密碼(create)
		add Views/Members/ConfirmResetPassword.cshtml //重新設定密碼確認



[]大頭照修改
>已經做好傳到資料夾
>上傳待處理，原本有先完成可以上傳字串待處理呈現

把自加JS寫到js資料夾

===============
**新增後台**