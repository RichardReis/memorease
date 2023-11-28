import React from "react";
import { View, Text, StyleSheet, TouchableOpacity } from "react-native";
import Constants from "expo-constants";
import TextStyles from "../../themedStyles/Text";
import Colors from "../../constants/Colors";
import Spacing from "../../constants/Spacing";
import Icon from "../Icon";
import { useRouter } from "expo-router";

interface DefaultScreenStructureProps {
  activeBackButton?: boolean;
  children?: React.ReactNode;
  config?: () => void;
  headerButton?: React.ReactNode;
  title: string;
}

const DefaultScreenStructure: React.FC<DefaultScreenStructureProps> = ({
  activeBackButton,
  children,
  config,
  headerButton,
  title,
}) => {
  const router = useRouter();

  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <View style={styles.headerTitle}>
          {activeBackButton && (
            <TouchableOpacity
              style={styles.backButton}
              onPress={() => router.back()}
            >
              <Icon name="arrow-left" color="white" />
            </TouchableOpacity>
          )}
          <Text style={{ ...TextStyles.titleHeader }}>{title}</Text>
          {config && (
            <TouchableOpacity style={styles.configButton} onPress={config}>
              <Icon name="circle-edit-outline" color="white" />
            </TouchableOpacity>
          )}
        </View>
        {headerButton}
      </View>
      <View style={styles.content}>{children}</View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    paddingTop: Constants.statusBarHeight,
    backgroundColor: Colors["light"].primary,
  },
  header: {
    flexDirection: "row",
    alignItems: "center",
    justifyContent: "space-between",

    padding: Spacing.m,
  },
  headerTitle: {
    flexDirection: "row",
    alignItems: "baseline",
  },
  configButton: {
    marginLeft: Spacing.s,
  },
  backButton: {
    marginRight: Spacing.s,
  },
  content: {
    flex: 1,
    backgroundColor: Colors["light"].contentBackground,
    borderTopRightRadius: Spacing.g,
    borderTopLeftRadius: Spacing.g,

    paddingTop: Spacing.g,
  },
});

export default DefaultScreenStructure;
