Imports System.Math
Public Class Char_class
  Inherits Def_char
  Public Sub New()
    MyBase.New
    name_string = "地球母亲"
    short_name = "地球"
    description = "主要是群海豹"
  End Sub

  Public Shared v30_pho(1) As Photo
  Public Shared v2_pho(1) As Photo
  Public Shared rock_pho(0) As Photo

  Private Shared Sub Load_resource(glb_ind_in As Integer)
    Static loaded As Boolean = False
    If Not loaded Then
      Dim v30_bm As SharpDX.Direct2D1.Bitmap1 = _D.Load_Bitmap1(Dll_char_info.info_list(glb_ind_in).dll_path & "vision30.png")
      For i = 0 To 1
        v30_pho(i) = New Photo(v30_bm, 0, 192 * i, 320, 192,, 0)
        v2_pho(i) = New Photo(v30_bm, 0, 400 + 32 * i, 320, 32,, 0)
      Next
      rock_pho(0) = New Photo(_D.Load_Bitmap1(Dll_char_info.info_list(glb_ind_in).dll_path & "hill.png"),
                             0, 0, 180, 180, 80)
      loaded = True
    End If
  End Sub
  Public Overrides Sub Char_ini(leader_in As Integer)
    Load_resource(global_ind)
    MyBase.Char_ini(leader_in)
    With zj(ld)
      .layer_list(1, 3) = Blend_type.Maximum
      .my_gauge = {New Gauge(1, 1, 1 / 180, 0, 60, "冲刺",,,, Gauge.Show_type.hide)}
      .source = pzj(1)
      .opacity = 0 '永不显示

      '判明第几关
      If mission_ind <= mission_list.Length - 1 Then '关卡编号在list范围内
        Prepair_new_map(mission_list(mission_ind).map_str)
        total_seal = mission_list(mission_ind).seals.Length
        remain_seal = total_seal
        tgt_seal = mission_list(mission_ind).tgt_seal_amount
        killed_seal = 0
      Else

      End If

    End With
  End Sub

  Public total_seal, remain_seal, tgt_seal, killed_seal As Integer


  Public Overrides Sub Char_Pshoot()
    Dim temp As Integer
    If t = 0 Then
      '生成石头
      If mission_ind <= mission_list.Length - 1 Then
        For Each elm In mission_list(mission_ind).rocks
          With elm
            temp = Bini(rock_pho(0), .x, .y, .ang, 0, zj(ld).dad, -2, global_ind, Bt.rock_1, 16)
            dm(temp).scale_y = .photo_r / (dm(temp).source.h / 2)
            dm(temp).scale_x = dm(temp).scale_y
            dm(temp).hit_r = dm(temp).source.hit_r * dm(temp).scale_y
            dm(temp).feature = New Rock_feat(temp) With {.ini_rock = elm}
          End With
        Next

        '生成海豹
        For Each elm In mission_list(mission_ind).seals
          With elm
            temp = Bini(pzj(4), .x, .y, .ini_ang, 0, zj(ld).dad, -1, global_ind, Bt.seal_1, 16,,, 1, 1)
            dm(temp).feature = New Seal_feat(temp) With {.ini_para = elm}
          End With
        Next

        Dim bear_ini_cord() As Double = mission_list(mission_ind).bear_cord
        zj(3 - ld).x = bear_ini_cord(0)
        zj(3 - ld).y = bear_ini_cord(1)
        zj(3 - ld).p_ang = bear_ini_cord(2)
      End If
    End If
  End Sub
  Public Overrides Sub Char_Bmove(index As Integer)
    With dm(index)
      'Dim temp As Double
      Select Case .type

      End Select
    End With
  End Sub
  Public Overrides Sub Char_Bdeath(index As Integer, more As Double)
    With dm(index)
      Select Case .type

        Case Else
          .B_real_delete()
      End Select
    End With
  End Sub

  Public Overrides Sub Char_gauge_show()
    'MyBase.Char_gauge_show()
    zj(ld).rt_line.Show_elms() '伪命令行
  End Sub

  Public Overrides Sub Char_Phitted(target As Integer) '判定胜负！！
    If total_seal > 0 AndAlso remain_seal = 0 Then
      If killed_seal >= tgt_seal Then
        winner = 1 '熊胜利
        mission_ind += 1
      Else
        winner = 2 '熊失败
      End If
    End If
  End Sub

  Public Enum Bt
    empty
    seal_1
    rock_1
    vision_cone
  End Enum
  Public Enum Sk
    nope
  End Enum
End Class


Public Class Seal_feat '海豹
  Inherits Def_feature
  Public Sub New(index As Integer)
    MyBase.New(index)
  End Sub
  Public ini_para As Seal_initial
  Public Shared ReadOnly cone_vision_r As Double = 300 '半径
  Public ReadOnly around_vision_r As Double = 100 '半径
  Public ReadOnly vision_angle_range As Double = 15 '从中线偏移的单侧视野角度
  Public alerted As Integer '警戒状态，开始警戒不会再恢复平静
  Public rotate_period As Integer
  'Public Shared ReadOnly rbear As Double = 20

  Public vcone(14) As Bullet

  Public Overrides Sub Bmove()
    With blt
      If .age = 0 Then
        For i = 0 To vcone.Length - 1
          Dim vision_cone_ind = Bini(Char_class.v2_pho(0), .x, .y, .p_ang, 0, .dad, -2,
                               dad_char.global_ind, Char_class.Bt.vision_cone, 0,, 0.25)
          dm(vision_cone_ind).feature = New Vision_feat(vision_cone_ind) With {
            .seal_b = blt,
            .seal_f = Me,
            .ang_diff = vision_angle_range * ((i + 0.5) / vcone.Length * 2 - 1)}
          dm(vision_cone_ind).shape = Collide_shape.rect
          dm(vision_cone_ind).hit_r = 1
          vcone(i) = dm(vision_cone_ind)
        Next
      End If


      Dim to_bear_ang As Double = Snipe(.x, .y, zj(3 - .dad).X1, zj(3 - .dad).Y1)
      Dim to_bear_dist As Double = Distance_c(.x, .y, zj(3 - .dad).X1, zj(3 - .dad).Y1)

      Dim x2 As Double = .x + Cosd(.p_ang + 180) * zj(3 - .dad).hit_r / Sind(vision_angle_range)
      Dim y2 As Double = .y - Sind(.p_ang + 180) * zj(3 - .dad).hit_r / Sind(vision_angle_range)
      Dim alert_bear_ang As Double = Snipe(x2, y2, zj(3 - .dad).X1, zj(3 - .dad).Y1)
      If alerted = 0 Then
        'Dim ang_temp As Double = Chase(.p_ang, alert_bear_ang, vision_angle_range)
        'ang_temp = IEEERemainder(ang_temp, 180)
        'If Abs(ang_temp) < vision_angle_range Then '角度在视野范围内
        '    Dim cone_bear_dist As Double = Distance_c(x2, y2, zj(3 - .dad).X1, zj(3 - .dad).Y1)
        '    If cone_bear_dist < cone_vision_r + rbear / Sind(vision_angle_range) + rbear Then
        '      alerted = 1 '警觉！
        '    End If
        'Else 

        '周身警觉
        If to_bear_dist < around_vision_r + zj(3 - .dad).hit_r Then
          alerted = 1 '警觉！
        End If
      End If
      'End If
      If alerted = 1 Then '警觉！
        .source = pzj(1) '变色
        .speed = Min(.speed + 0.1, 5)
        .mov_ang = to_bear_ang + 180
        .p_ang += Chase(.p_ang, .mov_ang, 6)
        '.p_ang = .mov_ang
      Else
        With ini_para
          If .freeze_sp > 0 AndAlso .swipe_sp > 0 Then
            Dim now_phase As Integer = (blt.age + .ini_phase) Mod (.freeze_sp + .swipe_sp) * 2
            Select Case now_phase
              Case Is <= .freeze_sp                          '不动
                blt.p_ang = .ini_ang
              Case Is <= .freeze_sp + .swipe_sp              '扫视
                blt.p_ang = .ini_ang + .turn_ang * (now_phase - .freeze_sp) / .swipe_sp
              Case Is <= .freeze_sp * 2 + .swipe_sp          '不动
                blt.p_ang = .ini_ang + .turn_ang
              Case Else                                      '扫视
                blt.p_ang = .ini_ang + .turn_ang * (1 - (now_phase - .freeze_sp * 2 - .swipe_sp) / .swipe_sp)
            End Select
          End If
        End With
      End If

      If Not Check_at_ground(.x, .y) Then
        '入水
        Bdeath(4)
      End If
    End With
  End Sub
  Public Overrides Sub Bdeath(more As Double)
    Dim temp As Integer
    With blt
      Select Case more
        Case 1 '出界
        Case 3 '咬死
          For i = 0 To 15
            temp = Bini(pdm(1, 1), .x, .y, 360 / 16 * i, 5, .dad, 4, DEF_CHAR_IND, Def_char.Bt_d.fade, 0)
            dm(temp).ls = 0.05
          Next
          DirectCast(dad_char, Char_class).killed_seal += 1
        Case 4 '入水
          For i = 0 To 15
            temp = Bini(pdm(1, 7), .x, .y, 360 / 16 * i, 5, .dad, 4, DEF_CHAR_IND, Def_char.Bt_d.fade, 0)
            dm(temp).ls = 0.05
          Next
        Case Else
          Exit Sub
      End Select
      For Each elm In vcone
        elm.Bdeath(3)
      Next
      DirectCast(dad_char, Char_class).remain_seal -= 1
      .B_real_delete()
    End With
  End Sub
End Class

Public Class Vision_feat '视锥
  Inherits Def_feature
  Public Sub New(index As Integer)
    MyBase.New(index)
  End Sub

  Public ang_diff As Double
  Public seal_b As Bullet
  Public seal_f As Seal_feat

  Public Overrides Sub Bmove()
    With blt
      .x = seal_b.x
      .y = seal_b.y
      .p_ang = seal_b.p_ang + ang_diff
      .length = Seal_feat.cone_vision_r
      Bullet.Traversal_bullet(.dad, AddressOf To_rock)
      .scale_x = .length / .source.w
      .scale_y = .scale_x

      If seal_f.alerted = 0 Then
        If .Collide(zj(3 - .dad).x, zj(3 - .dad).y, zj(3 - .dad).hit_r) Then
          seal_f.alerted = 1
        End If
      End If

      If seal_f.alerted = 1 Then
        .source = Char_class.v2_pho(1)
        .opacity -= 0.02
      End If
    End With
  End Sub
  Public Sub To_rock(ind As Integer)
    With dm(ind)
      If .type <> Char_class.Bt.rock_1 Then
        Exit Sub
      End If '只找石头
      Dim a As Double = blt.p_ang
      Dim a_rock As Double = Snipe(blt.x, blt.y, .x, .y)
      Dim d As Double = Distance_c(blt.x, blt.y, .x, .y)
      Dim theta As Double = Abs(IEEERemainder(a - a_rock, 180))
      If theta > 90 Then Exit Sub

      Dim ver As Double = d * Sind(theta)
      If ver > 0 AndAlso ver < .hit_r Then
        Dim hor As Double = d * Cosd(theta)
        Dim tgt_len As Double = hor - Sqrt(.hit_r * .hit_r - ver * ver)
        If blt.length > tgt_len Then blt.length = tgt_len
      End If
    End With
  End Sub
End Class


Public Class Rock_feat '石头
  Inherits Def_feature
  Public Sub New(index As Integer)
    MyBase.New(index)
  End Sub
  Public ini_rock As Rock_initial

  Public Shared bear_box_r As Double = 20

  Public Overrides Sub Bmove()
    With blt
      If zj(3 - .dad).my_gauge(2).cd > 0 Then Exit Sub '跳起时不判定
      Dim dist As Double = Distance_c(.x, .y, zj(3 - .dad).X1, zj(3 - .dad).Y1)
      If dist < .hit_r + bear_box_r Then
        Dim push_l As Double = (dist - (.hit_r + bear_box_r)) / 4
        Dim ang As Double = 180 + Snipe(.x, .y, zj(3 - .dad).X1, zj(3 - .dad).Y1)
        zj(3 - .dad).x += Cosd(ang) * push_l
        zj(3 - .dad).y -= Sind(ang) * push_l
      End If
    End With
  End Sub


End Class
