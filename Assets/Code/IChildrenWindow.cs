using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IChildrenWindow
{
    void DoWindow(int id);
    bool IsOpened();
    void SetOpened(bool opened);
}
