Public Module Missions_design
  Public Structure Mission
    Dim map_str As String
    Dim bear_cord() As Double '熊的坐标{x,y,朝向}(向右为0，逆时针360度一圈)
    Dim seals() As Seal_initial '海豹数组
    Dim rocks() As Rock_initial '石头数组
    Dim tgt_seal_amount As Integer '获胜需要吃的海豹数量
  End Structure
  Public Structure Seal_initial '海豹初始参数
    Dim x As Double '横坐标,场地全宽1920
    Dim y As Double '纵坐标,场地全长1080
    Dim ini_ang As Double '初始角度，0~360，负数什么的也行
    Dim turn_ang As Double '转动角度，逆时针为正向
    Dim freeze_sp As Integer '静止时长
    Dim swipe_sp As Integer '转动时长
    Dim ini_phase As Integer '初始相位，一个周期是 freeze +swipe +freeze +swipe
  End Structure
  Public Structure Rock_initial '石头初始参数
    Dim x As Double '横坐标,场地全宽1920
    Dim y As Double '纵坐标,场地全长1080
    Dim photo_r As Double '图片半径
    'Dim collide_r As Double '碰撞判定半径，自动套用图片的就行了
    Dim ang As Double '图片转动角度
  End Structure

    Public mission_list() As Mission = {
    New Mission With {
    .map_str = "
100111001111011111011
100111001111111111110
111111111001111110011
011111111001111111111
111011111011111111110
111111111111111111111
011111111110111111011
011111101110111111001
110011101111111111001
101111111111110111111
111111111111110111011
001111110111111111001





",
  .bear_cord = {1800, 60, 270},
  .seals = {
New Seal_initial With {.x = 960, .y = 100, .ini_ang = 0},
New Seal_initial With {.x = 1020, .y = 300, .ini_ang = 0},
    New Seal_initial With {.x = 600, .y = 600, .ini_ang = 340},
     New Seal_initial With {.x = 600, .y = 1000, .ini_ang = 90， .turn_ang = -90, .freeze_sp = 80, .swipe_sp = 60},
          New Seal_initial With {.x = 550, .y = 900, .ini_ang = 30},
      New Seal_initial With {.x = 1400, .y = 550, .ini_ang = 15},
       New Seal_initial With {.x = 1600, .y = 850, .ini_ang = 70},
              New Seal_initial With {.x = 1500, .y = 850, .ini_ang = 0， .turn_ang = -90, .freeze_sp = 60, .swipe_sp = 60},
       New Seal_initial With {.x = 1600, .y = 980, .ini_ang = 0},
       New Seal_initial With {.x = 1680, .y = 350, .ini_ang = 60}，
    New Seal_initial With {.x = 800, .y = 750, .ini_ang = 90, .turn_ang = -90, .freeze_sp = 60, .swipe_sp = 60},
    New Seal_initial With {.x = 960, .y = 900, .ini_ang = 120, .turn_ang = 90, .freeze_sp = 80, .swipe_sp = 60},
    New Seal_initial With {.x = 700, .y = 500, .ini_ang = 60},
    New Seal_initial With {.x = 960, .y = 330, .ini_ang = 60}
  },'seals
  .rocks = {
    New Rock_initial With {.x = 960, .y = 540, .photo_r = 100, .ang = 20},
       New Rock_initial With {.x = 200, .y = 200, .photo_r = 50, .ang = 20},
          New Rock_initial With {.x = 250, .y = 200, .photo_r = 50, .ang = 20},
                    New Rock_initial With {.x = 300, .y = 200, .photo_r = 50, .ang = 20},
                              New Rock_initial With {.x = 200, .y = 250, .photo_r = 50, .ang = 20},
                                        New Rock_initial With {.x = 200, .y = 300, .photo_r = 50, .ang = 20},
             New Rock_initial With {.x = 400, .y = 540, .photo_r = 50, .ang = 20},
      New Rock_initial With {.x = 400, .y = 540, .photo_r = 50, .ang = 20},
        New Rock_initial With {.x = 450, .y = 540, .photo_r = 50, .ang = 20},
          New Rock_initial With {.x = 500, .y = 540, .photo_r = 50, .ang = 20},
            New Rock_initial With {.x = 550, .y = 540, .photo_r = 50, .ang = 20},
                   New Rock_initial With {.x = 650, .y = 490, .photo_r = 50, .ang = 20},
                      New Rock_initial With {.x = 650, .y = 440, .photo_r = 50, .ang = 20},
                         New Rock_initial With {.x = 650, .y = 390, .photo_r = 50, .ang = 20},
      New Rock_initial With {.x = 400, .y = 800, .photo_r = 50, .ang = 20},
       New Rock_initial With {.x = 450, .y = 800, .photo_r = 50, .ang = 20},
        New Rock_initial With {.x = 500, .y = 800, .photo_r = 50, .ang = 20},
         New Rock_initial With {.x = 550, .y = 800, .photo_r = 50, .ang = 20},
          New Rock_initial With {.x = 600, .y = 800, .photo_r = 50, .ang = 20},
           New Rock_initial With {.x = 650, .y = 800, .photo_r = 50, .ang = 20},
    New Rock_initial With {.x = 1400, .y = 600, .photo_r = 50, .ang = 20},
        New Rock_initial With {.x = 1400, .y = 650, .photo_r = 50, .ang = 20},
        New Rock_initial With {.x = 1400, .y = 700, .photo_r = 50, .ang = 20},
            New Rock_initial With {.x = 1400, .y = 750, .photo_r = 50, .ang = 20},
                New Rock_initial With {.x = 1400, .y = 800, .photo_r = 50, .ang = 20},
                    New Rock_initial With {.x = 1400, .y = 850, .photo_r = 50, .ang = 20},
    New Rock_initial With {.x = 1400, .y = 900, .photo_r = 50, .ang = 20},
        New Rock_initial With {.x = 1400, .y = 950, .photo_r = 50, .ang = 20},
     New Rock_initial With {.x = 1400, .y = 1000, .photo_r = 50, .ang = 20},
         New Rock_initial With {.x = 1400, .y = 1050, .photo_r = 50, .ang = 20},
      New Rock_initial With {.x = 1200, .y = 1100, .photo_r = 50, .ang = 20}，
      New Rock_initial With {.x = 1250, .y = 300, .photo_r = 50, .ang = 20},
      New Rock_initial With {.x = 1300, .y = 300, .photo_r = 50, .ang = 20},
      New Rock_initial With {.x = 1350, .y = 300, .photo_r = 50, .ang = 20}，
      New Rock_initial With {.x = 1400, .y = 300, .photo_r = 50, .ang = 20}，
      New Rock_initial With {.x = 1450, .y = 300, .photo_r = 50, .ang = 20}，
      New Rock_initial With {.x = 1450, .y = 25, .photo_r = 50, .ang = 20}，
      New Rock_initial With {.x = 1500, .y = 25, .photo_r = 50, .ang = 20}，
        New Rock_initial With {.x = 1550, .y = 25, .photo_r = 50, .ang = 20}，
          New Rock_initial With {.x = 1600, .y = 25, .photo_r = 50, .ang = 20}，
            New Rock_initial With {.x = 1650, .y = 25, .photo_r = 50, .ang = 20}，
              New Rock_initial With {.x = 1700, .y = 25, .photo_r = 50, .ang = 20}，
                New Rock_initial With {.x = 1900, .y = 350, .photo_r = 50, .ang = 20}，
                  New Rock_initial With {.x = 1900, .y = 400, .photo_r = 50, .ang = 20}，
                    New Rock_initial With {.x = 1900, .y = 450, .photo_r = 50, .ang = 20}，
                      New Rock_initial With {.x = 1900, .y = 500, .photo_r = 50, .ang = 20}，
                        New Rock_initial With {.x = 1900, .y = 550, .photo_r = 50, .ang = 20}，
                          New Rock_initial With {.x = 1900, .y = 600, .photo_r = 50, .ang = 20}，
                           New Rock_initial With {.x = 1100, .y = 1100, .photo_r = 50, .ang = 20}，
                            New Rock_initial With {.x = 1150, .y = 1100, .photo_r = 50, .ang = 20}，
                             New Rock_initial With {.x = 1200, .y = 1100, .photo_r = 50, .ang = 20}，
                              New Rock_initial With {.x = 1250, .y = 1100, .photo_r = 50, .ang = 20}，
                               New Rock_initial With {.x = 1300, .y = 1100, .photo_r = 50, .ang = 20}
  },'rocks
  .tgt_seal_amount = 6
  }'mission(0)
  } '所有mission

End Module
