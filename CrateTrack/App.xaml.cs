﻿using CrateTrack.Database;

namespace CrateTrack;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

        MainPage = new NavigationPage(new MainPage());
    }
}
