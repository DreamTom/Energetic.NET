-- ----------------------------
-- Records of sys_resource
-- ----------------------------
INSERT INTO `sys_resource` VALUES (1194575264235913216, '系统管理', '', 0, '', '/system', 'layui-icon-set', 1000, 1, 1, '', 'GET', 1194574776488689664, '', '2024-01-10 17:35:37.158', NULL, NULL, NULL, NULL, NULL, NULL, 0);
INSERT INTO `sys_resource` VALUES (1194575373468172288, '用户管理', '', 1194575264235913216, '1194575264235913216', '/system/user', 'layui-icon-user', 1000, 2, 1, '', 'GET', 1194574776488689664, '', '2024-01-10 17:36:03.175', NULL, NULL, NULL, NULL, NULL, NULL, 0);
INSERT INTO `sys_resource` VALUES (1194575509514616832, '新增', 'sys:users:add', 1194575373468172288, '1194575264235913216/1194575373468172288', '', '', 1000, 3, 1, '/api/users', 'POST', 1194574776488689664, '', '2024-01-10 17:36:35.609', NULL, NULL, NULL, NULL, NULL, NULL, 0);
INSERT INTO `sys_resource` VALUES (1194575628381192192, '编辑', 'sys:users:edit', 1194575373468172288, '1194575264235913216/1194575373468172288', '', '', 1001, 3, 1, '/api/users', 'PUT', 1194574776488689664, '', '2024-01-10 17:37:03.949', NULL, NULL, NULL, NULL, NULL, NULL, 0);
INSERT INTO `sys_resource` VALUES (1194575799890477056, '角色管理', '', 1194575264235913216, '1194575264235913216', '/system/role', 'layui-icon-password', 1001, 2, 1, '', 'GET', 1194574776488689664, '', '2024-01-10 17:37:44.840', NULL, NULL, NULL, NULL, NULL, NULL, 0);

-- ----------------------------
-- Records of sys_role
-- ----------------------------
INSERT INTO `sys_role` VALUES (1194574882155790336, '超级管理员', 'superadmin', '12323', 1194574776488689664, '', '2024-01-10 17:34:06.052', NULL, NULL, NULL, NULL, NULL, NULL, 0);
INSERT INTO `sys_role` VALUES (1194575007049580544, '管理员', 'admin', '121', 1194574776488689664, '', '2024-01-10 17:34:35.812', NULL, NULL, NULL, NULL, NULL, NULL, 0);

-- ----------------------------
-- Records of sys_user
-- ----------------------------
INSERT INTO `sys_user` VALUES (1194574776488689664, NULL, '3443967655@qq.com', NULL, NULL, '3443967655@qq.com', 0, NULL, 0, 2, NULL, 0, '', '2024-01-10 17:33:40.967', NULL, NULL, NULL, NULL, NULL, NULL, 0);