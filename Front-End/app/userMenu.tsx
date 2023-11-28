import React from "react";
import { View, StyleSheet, Text, TouchableOpacity } from "react-native";
import Constants from "expo-constants";
import Colors from "../constants/Colors";
import { Stack, useRouter } from "expo-router";
import Icon from "../components/Icon";
import DefaultScreenStructure from "../components/DefaultScreenStructure";
import Spacing from "../constants/Spacing";
import TextStyles from "../themedStyles/Text";
import { MaterialCommunityIcons } from "@expo/vector-icons";

type MenuItemProps = {
  text: string;
  href: string;
  icon: React.ComponentProps<typeof MaterialCommunityIcons>["name"];
};

const UserMenu: React.FC = () => {
  const router = useRouter();

  const MenuItem = ({ href, text, icon }: MenuItemProps) => {
    return (
      <>
        <TouchableOpacity style={styles.item} onPress={() => router.push(href)}>
          <Icon color={Colors["light"].text} name={icon} size={30} />
          <Text style={styles.itemtext}>{text}</Text>
        </TouchableOpacity>
        <View style={styles.separator} />
      </>
    );
  };

  return (
    <>
      <Stack.Screen options={{ headerShown: false }} />
      <DefaultScreenStructure title="Gerenciar conta" activeBackButton>
        <View style={styles.container}>
          <MenuItem
            href="/editProfile"
            text="Alterar Dados"
            icon="account-cog"
          />
          <MenuItem href="/changePassword" text="Alterar Senha" icon="key" />
          <MenuItem
            href="/deleteAccount"
            text="Deletar Conta"
            icon="account-remove"
          />
          <MenuItem href="/" text="Sair" icon="logout" />
        </View>
      </DefaultScreenStructure>
    </>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors["light"].contentBackground,
    paddingTop: Constants.statusBarHeight,
  },
  item: {
    width: "100%",
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "center",
    padding: Spacing.g,
  },
  itemtext: {
    ...TextStyles.labelBold,
    fontSize: 24,
    marginLeft: Spacing.g,
  },
  separator: {
    borderBottomWidth: 2,
    borderColor: Colors["light"].text,
    marginHorizontal: Spacing.g,
  },
});

export default UserMenu;
