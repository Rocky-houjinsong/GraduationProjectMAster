﻿namespace ToRead.Services;

public interface IAlertService
{
    void Alert(string title, string message, string button);
}