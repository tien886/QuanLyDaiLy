﻿
using QuanLyDaiLy.Models;

namespace QuanLyDaiLy.Interfaces;

public interface IThamSoRepository
{
    Task<string> GetThamSo(string key);
}
