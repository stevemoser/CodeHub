﻿using System;
using Foundation;
using UIKit;
using CodeHub.Core.ViewModels.Users;
using ReactiveUI;
using System.Reactive.Linq;
using CoreGraphics;

namespace CodeHub.iOS.Cells
{
    public partial class UserTableViewCell : ReactiveTableViewCell<UserItemViewModel>
    {
        public static readonly NSString Key = new NSString("UserTableViewCell");
        private const float ImageSpacing = 10f;
        private static CGRect ImageFrame = new CGRect(ImageSpacing, 6f, 32, 32);

        public UserTableViewCell(IntPtr handle)
            : base(handle)
        {
            SeparatorInset = new UIEdgeInsets(0, ImageFrame.Right + ImageSpacing, 0, 0);
            ImageView.Layer.CornerRadius = ImageFrame.Height / 2f;
            ImageView.Layer.MasksToBounds = true;
            ImageView.ContentMode = UIViewContentMode.ScaleAspectFill;
            ContentView.Opaque = true;

            SeparatorInset = new UIEdgeInsets(0, TextLabel.Frame.X, 0, 0);

            this.OneWayBind(ViewModel, x => x.Name, x => x.TextLabel.Text);

            this.WhenAnyValue(x => x.ViewModel)
                .IsNotNull()
                .Select(x => x.Avatar)
                .Subscribe(x => ImageView.SetAvatar(x));
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            ImageView.Frame = ImageFrame;
            TextLabel.Frame = new CGRect(ImageFrame.Right + ImageSpacing, TextLabel.Frame.Y, TextLabel.Frame.Width, TextLabel.Frame.Height);
        }
    }
}

